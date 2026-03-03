using ErrorOr;
using HR_Platform.Application.BrigadeInventories.MarkAsDeleted;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BrigadeInventorys.Update;

internal sealed class MarkBrigateInventoryAsDeletedCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IBrigadeInventoryRepository brigadeInventoryRepository,
    IBrigadeInventoryFileRepository brigadeInventoryFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<MarkBrigateInventoryAsDeletedCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IBrigadeInventoryRepository _brigadeInventoryRepository = brigadeInventoryRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryRepository));
    private readonly IBrigadeInventoryFileRepository _brigadeInventoryFileRepository = brigadeInventoryFileRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(MarkBrigateInventoryAsDeletedCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianHour = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = colombianHour.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _brigadeInventoryRepository.GetByIdAsync(new BrigadeInventoryId(command.Id)) is not BrigadeInventory oldBrigadeInventory)
            return Error.NotFound("BrigadeInventory.NotFound", "The Brigade Inventory with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("BrigadeInventory.EditionDate", "EditionDate is not valid");

        try
        {

            oldBrigadeInventory.IsDeleted = true;
            oldBrigadeInventory.EditionDate = editionDate;

            _brigadeInventoryRepository.Update(oldBrigadeInventory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}