using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.NewCommunications.Create;

internal sealed class CreateNewCommunicationsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    INewCommunicationRepository newCommunicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateNewCommunicationsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly INewCommunicationRepository _newCommunicationRepository = newCommunicationRepository ?? throw new ArgumentNullException(nameof(newCommunicationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateNewCommunicationsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("NewCommunications.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        NewCommunication result = new
        (
            new NewCommunicationId(Guid.NewGuid()),
            oldCompany.Id,
            command.Name,
            command.Description,
            !string.IsNullOrEmpty(command.FileName) ? command.FileName : string.Empty,
            !string.IsNullOrEmpty(command.FileURL) ? command.FileURL: string.Empty,
            creationDate, // creationDateFile
            !string.IsNullOrEmpty(command.ImageName ) ? command.ImageName : string.Empty,
            !string.IsNullOrEmpty(command.ImageURL)? command.ImageURL : string.Empty,
            command.IsSurveyInclude,
            command.EmailChangeBy,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
            false, //IsVisible
            true, //IsEditable
            true, //IsDeletable
            creationDate,
            creationDate
       );

        try
        {
            _newCommunicationRepository.Add(result);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}