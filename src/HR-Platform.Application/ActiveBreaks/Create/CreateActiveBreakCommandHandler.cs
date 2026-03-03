using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using HR_Platform.Domain.ActiveBreaks;

namespace HR_Platform.Application.ActiveBreaks.Create;

internal sealed class CreateActiveBreakCommandHandler
(
    IActiveBreakRepository activeBreakRepository,
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<CreateActiveBreakCommand, ErrorOr<bool>>
{

    private readonly IActiveBreakRepository _activeBreakRepository = activeBreakRepository ?? throw new ArgumentNullException(nameof(activeBreakRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateActiveBreakCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Benefits.CreationDate", "CreationDate is not valid");

        Collaborator? collaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailWhoChangedByHR);

        #pragma warning disable CS8604 // Posible argumento de referencia nulo

        ActiveBreak result = new
        (
            new ActiveBreakId(Guid.NewGuid()),

            oldCompany.Id,

            command.Name,

            command.Description,

            !string.IsNullOrEmpty(command.ImageURL) ? command.ImageURL : string.Empty,
            !string.IsNullOrEmpty(command.ImageName) ? command.ImageName : string.Empty,

            !string.IsNullOrEmpty(command.FileURL) ? command.FileURL : string.Empty,
            !string.IsNullOrEmpty(command.FileName) ? command.FileName : string.Empty,

            command.EmailWhoChangedByHR,
            collaboratorWhoChanged?.Name,

            false, //IsVisible
            false, //IsPinned

            true, //IsEditable
            true, //IsDeletable

            !string.IsNullOrEmpty(command.ImageURL) ? creationDate : null, // CreationDateImage
            !string.IsNullOrEmpty(command.FileURL) ? creationDate : null, // CreationDateFile

            creationDate, // CreationDate
            creationDate // EditionDate 
        );

        #pragma warning restore CS8604 // Posible argumento de referencia nulo

        try
        {
            _activeBreakRepository.Add(result);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}