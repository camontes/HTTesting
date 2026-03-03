using ErrorOr;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.UnitMeasures;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.CollaboratorBrigadeInventories.Create;

internal sealed class CreateCollaboratorBrigadeInventoryCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorBrigadeRepository collaboratorBrigadeRepository,
    IBrigadeInventoryRepository brigadeInventoryRepository,
    IBrigadeMemberRepository brigadeMemberRepository,
    ICollaboratorBrigadeInventoryRepository collaboratorBrigadeInventoryRepository,
    ICollaboratorBrigadeInventoryFileRepository collaboratorBrigadeInventoryFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCollaboratorBrigadeInventoryCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorBrigadeRepository _collaboratorBrigadeRepository = collaboratorBrigadeRepository ?? throw new ArgumentNullException(nameof(collaboratorBrigadeRepository));
    private readonly IBrigadeInventoryRepository _brigadeInventoryRepository = brigadeInventoryRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryRepository));
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly ICollaboratorBrigadeInventoryRepository _collaboratorBrigadeInventoryRepository = collaboratorBrigadeInventoryRepository ?? throw new ArgumentNullException(nameof(collaboratorBrigadeInventoryRepository));
    private readonly ICollaboratorBrigadeInventoryFileRepository _collaboratorBrigadeInventoryFileRepository = collaboratorBrigadeInventoryFileRepository ?? throw new ArgumentNullException(nameof(collaboratorBrigadeInventoryFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateCollaboratorBrigadeInventoryCommand command, CancellationToken cancellationToken)
    {
        #region Date Formats

        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("CollaboratorBrigadeInventory.CreationDate", "CreationDate is not valid");

        DateTime MinDateFormat = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.MinValue, "SA Pacific Standard Time");
        string MinDateFormatTemp = MinDateFormat.ToString("MM/dd/yyyy HH:mm:ss");

        string deliveryString = string.Empty;
        string returnString = string.Empty;

        if (!string.IsNullOrEmpty(command.DeliveryDate))
        {
            DateTime temp = DateTime.ParseExact(command.DeliveryDate, "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            deliveryString = temp.ToString("MM/dd/yyyy HH:mm:ss");
        }

        if (!string.IsNullOrEmpty(command.ReturnDate))
        {
            DateTime temp2 = DateTime.ParseExact(command.ReturnDate, "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            returnString = temp2.ToString("MM/dd/yyyy HH:mm:ss");
        }

        string? deliveryDateTemp = !string.IsNullOrEmpty(deliveryString)
                                  ? deliveryString
                                  : MinDateFormatTemp;

        string? returnDateTemp = command.ApplyReturnDate && !string.IsNullOrEmpty(returnString)
                                    ? returnString
                                    : MinDateFormatTemp;
        // If It is True will be Visible
        if (TimeDate.Create(deliveryDateTemp) is not TimeDate deliveryDate)
            return Error.Validation("CollaboratorBrigadeInventory.DeliveryDate", "DeliveryDate Format is not valid");

        if (TimeDate.Create(returnDateTemp) is not TimeDate returnDate)
            return Error.Validation("CollaboratorBrigadeInventory.ReturnDate", "ReturnDate Format is not valid");

        #endregion

        #region Validations
        int collaboratorCount = command.BrigadeMemberIdList.Count;

        if (!command.SendAllBrigades && collaboratorCount == 0)
            return Error.Validation("CollaboratorBrigadeInventory.BrigadeMember", "Must add at least one brigade member");

        List<BrigadeMember> brigaderMembers = await _brigadeMemberRepository.GetAll();
        if (command.SendAllBrigades && brigaderMembers.Count == 0)
            return Error.Validation("CollaboratorBrigadeInventory.BrigadeMember", "There are not brigade members");

        if (await _brigadeInventoryRepository.GetByIdAsync(new BrigadeInventoryId(command.GeneralBrigadeInventoryId)) is not BrigadeInventory oldBrigadeInventory)
            return Error.Validation("CollaboratorBrigadeInventory.BrigadeInventoryId", "The General Brigade Inventory with the provide ID was not found");

        int AmountTimesCollaborator = command.Amount * collaboratorCount;
        if (AmountTimesCollaborator > oldBrigadeInventory.AvailableAmount)
            return Error.Validation("CollaboratorBrigadeInventory.AvailableAmount", "Insufficient inventory");

        #endregion

        #region Instances & Calls

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);
        List<CollaboratorBrigadeInventoryFile> collaboratorBrigadeInventoryFiles = [];
        List<CollaboratorBrigade> collaboratorBrigades = [];

        #endregion

        #region Create CollaboratorBrigadeInventory

        CollaboratorBrigadeInventory collaboratorBrigadeInventoryResult = new
        (
            new CollaboratorBrigadeInventoryId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),
            oldBrigadeInventory.Id,
            command.SendAllBrigades,
            AmountTimesCollaborator,
            deliveryDate,
            returnDate,
            new UnitMeasureId(command.UnitMeasureId),
            command.Observations is not null ? command.Observations : string.Empty,
            command.EmailChangeBy,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
            true,
            true,
            creationDate,
            creationDate
        );
        _collaboratorBrigadeInventoryRepository.Add(collaboratorBrigadeInventoryResult);

        #endregion

        #region Create Files to CollaboratorBrigadeInventory
        if (command.BrigadeInventoriesFileList.Count > 0)
        {
            foreach (CollaboratorBrigadeInventoryObject item in command.BrigadeInventoriesFileList)
            {
                CollaboratorBrigadeInventoryFile temp = new
                (
                    new CollaboratorBrigadeInventoryFileId(Guid.NewGuid()),
                    collaboratorBrigadeInventoryResult.Id,
                    item.FileName,
                    item.UrlFile,
                    true, //IsEditable
                    true, //IsDeletable
                    creationDate,
                    creationDate
                );
                collaboratorBrigadeInventoryFiles.Add(temp);
            }
        }

        if (collaboratorBrigadeInventoryFiles.Count > 0)
        {
            _collaboratorBrigadeInventoryFileRepository.AddRangeCollaboratorBrigadeInventoryFiles(collaboratorBrigadeInventoryFiles);
        }
        #endregion

        #region Create an association of CollaboratorBrigade with CollaboratorBrigadeInventory

        var collaborators = command.SendAllBrigades
         ? brigaderMembers.Select(col => col.Id.Value)
         : command.BrigadeMemberIdList;

        foreach (var itemId in collaborators)
        {
            BrigadeMember? tempBrigadeMember = await _brigadeMemberRepository.GetByIdAsync(new BrigadeMemberId(itemId));
            if (tempBrigadeMember is not null)
            {
                CollaboratorBrigade brigader = new
                (
                    new CollaboratorBrigadeId(Guid.NewGuid()),
                    collaboratorBrigadeInventoryResult.Id,
                    tempBrigadeMember.CollaboratorId,
                    tempBrigadeMember.BrigadeAdjustmentId,
                    command.Amount,
                    true, // IsEditable
                    true, // IsDeletable
                    creationDate,
                    creationDate
                );
                collaboratorBrigades.Add(brigader);
            }
        }
        _collaboratorBrigadeRepository.AddRangeCollaboratorBrigades(collaboratorBrigades);
        #endregion

        #region Update Inventary Account

        oldBrigadeInventory.AvailableAmount -= AmountTimesCollaborator;
        oldBrigadeInventory.EditionDate = creationDate;
        _brigadeInventoryRepository.Update(oldBrigadeInventory);

        #endregion


        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}