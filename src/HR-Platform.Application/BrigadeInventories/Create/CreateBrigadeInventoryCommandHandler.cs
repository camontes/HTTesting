using ErrorOr;
using HR_Platform.Application.BrigadeInventories.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using HR_Platform.Domain.UnitMeasures;
using System.Globalization;

namespace HR_Platform.Application.BrigadeInventorys.Create;

internal sealed class CreateBrigadeInventorysCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IBrigadeInventoryRepository brigadeInventoryRepository,
    IBrigadeInventoryFileRepository brigadeInventoryFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateBrigadeInventoriesCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IBrigadeInventoryRepository _brigadeInventoryRepository = brigadeInventoryRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryRepository));
    private readonly IBrigadeInventoryFileRepository _brigadeInventoryFileRepository = brigadeInventoryFileRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBrigadeInventoriesCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BrigadeInventorys.CreationDate", "CreationDate is not valid");

        DateTime MinDateFormat = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.MinValue, "SA Pacific Standard Time");
        string MinDateFormatTemp = MinDateFormat.ToString("MM/dd/yyyy HH:mm:ss");

        string purchaseString = string.Empty;
        string expirationString = string.Empty;

        if (!string.IsNullOrEmpty(command.PurchaseDate))
        {
            DateTime temp = DateTime.ParseExact(command.PurchaseDate, "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            purchaseString = temp.ToString("MM/dd/yyyy HH:mm:ss");
        }
        
        if (!string.IsNullOrEmpty(command.ExpirationDate))
        {
            DateTime temp2 = DateTime.ParseExact(command.ExpirationDate, "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            expirationString = temp2.ToString("MM/dd/yyyy HH:mm:ss");
        }

        string? purchaseDateTemp = command.ApplyPurchaseDate && !string.IsNullOrEmpty(purchaseString)
                                  ? purchaseString
                                  : MinDateFormatTemp;

        string? expirationDateTemp = command.ApplyExpirationDate && !string.IsNullOrEmpty(expirationString)
                                    ? expirationString
                                    : MinDateFormatTemp;


        // If It is True will be Visible
        if (TimeDate.Create(purchaseDateTemp) is not TimeDate purchaseDate)
            return Error.Validation("BrigadeInventory.PurchaseDate", "PurchaseDate Format is not valid");

        if (TimeDate.Create(expirationDateTemp) is not TimeDate expirationDate)
            return Error.Validation("BrigadeInventory.ExpirationDate", "ExpirationDate Format is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<BrigadeInventoryFile> brigadeInventoryFiles = [];

        BrigadeInventory brigadeInventoryResult = new
        (
            new BrigadeInventoryId(Guid.NewGuid()),
            oldCompany.Id,
            command.Name,
            command.Description,
            command.CompanyLocation,
            command.Amount == 0 ? 1 : command.Amount,
            command.Amount == 0 ? 1 : command.Amount, //AvailableAmount
            new UnitMeasureId(command.UnitMeasureId),
            purchaseDate,
            expirationDate,
            command.Observations is not null ? command.Observations : string.Empty,
            command.EmailChangeBy,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
            true, // IsEditable
            true, // IsDeleteable
            false, // IsDeleted
            creationDate,
            creationDate
        );

        _brigadeInventoryRepository.Add(brigadeInventoryResult);


        if (command.BrigadeInventoriesList.Count > 0)
        {
            foreach (CreateBrigadeInventoriesObjectCommand item in command.BrigadeInventoriesList)
            {
                BrigadeInventoryFile temp = new
                (
                    new BrigadeInventoryFileId(Guid.NewGuid()),
                    brigadeInventoryResult.Id,
                    item.FileName,
                    item.UrlFile,
                    true,
                    true,
                    creationDate,
                    creationDate
                );
                brigadeInventoryFiles.Add(temp);
            }
        }

        if (brigadeInventoryFiles.Count > 0)
        {
            _brigadeInventoryFileRepository.AddRangeBrigadeInventoryFiles(brigadeInventoryFiles);
        }

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