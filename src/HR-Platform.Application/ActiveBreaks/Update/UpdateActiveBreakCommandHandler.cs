using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using HR_Platform.Domain.ActiveBreaks;

namespace HR_Platform.Application.ActiveBreaks.Update;

internal sealed class UpdateActiveBreakCommandHandler
(
    IActiveBreakRepository activeBreakRepository,
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<UpdateActiveBreakCommand, ErrorOr<bool>>
{

    private readonly IActiveBreakRepository _activeBreakRepository = activeBreakRepository ?? throw new ArgumentNullException(nameof(activeBreakRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateActiveBreakCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianHour = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = colombianHour.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("ActiveBreaks.EditionDate", "EditionDate is not valid");

        Collaborator? collaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailWhoChangedByHR);

        if (await _activeBreakRepository.GetByIdAsync(new ActiveBreakId(command.Id)) is not ActiveBreak oldActiveBreak)
            return Error.NotFound("ActiveBreak.NotFound", "The ActiveBreak with the provide Id was not found.");

#pragma warning disable CS8604 // Posible argumento de referencia nulo

        ActiveBreak result = new
        (
            oldActiveBreak.Id,

            oldCompany.Id,

            !string.IsNullOrEmpty(command.Name) ? command.Name : oldActiveBreak.Name,

            !string.IsNullOrEmpty(command.Description) ? command.Description : oldActiveBreak.Description,

            !string.IsNullOrEmpty(command.ImageURL) ? command.ImageURL : string.Empty,
            !string.IsNullOrEmpty(command.ImageName) ? command.ImageName : string.Empty,

            !string.IsNullOrEmpty(command.FileURL) ? command.FileURL : string.Empty,
            !string.IsNullOrEmpty(command.FileName) ? command.FileName : string.Empty,

            command.EmailWhoChangedByHR,
            collaboratorWhoChanged?.Name,

            oldActiveBreak.IsVisible, //IsVisible
            oldActiveBreak.IsPinned, //IsPinned

            oldActiveBreak.IsEditable, //IsEditable
            oldActiveBreak.IsDeleteable, //IsDeletable

            !string.IsNullOrEmpty(command.ImageURL) ? editionDate : oldActiveBreak.EditionDate, // CreationDateImage
            !string.IsNullOrEmpty(command.FileURL) ? editionDate : oldActiveBreak.EditionDate, // CreationDateFile

            oldActiveBreak.CreationDate, // CreationDate
            editionDate // EditionDate 
        );

#pragma warning restore CS8604 // Posible argumento de referencia nulo

        try
        {
            _activeBreakRepository.Update(result);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}