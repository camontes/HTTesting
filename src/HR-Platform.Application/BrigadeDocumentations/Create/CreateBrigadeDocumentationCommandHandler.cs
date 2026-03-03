using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BrigadeDocumentations.Create;

internal sealed class CreateBrigadeDocumentationsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IBrigadeDocumentationRepository BrigadeDocumentationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateBrigadeDocumentationsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IBrigadeDocumentationRepository _minuteRepository = BrigadeDocumentationRepository ?? throw new ArgumentNullException(nameof(BrigadeDocumentationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBrigadeDocumentationsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BrigadeDocumentations.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<BrigadeDocumentation> minutes = [];

        foreach (CreateBrigadeDocumentationsObjectCommand item in command.BrigadeDocumentationsList)
        {
            BrigadeDocumentation result = new
            (
                new BrigadeDocumentationId(Guid.NewGuid()),
                oldCompany.Id,
                item.FileName,
                item.UrlFile,
                command.EmailChangeBy,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                true,
                true,
                creationDate,
                creationDate
            );
            minutes.Add(result);
        }

        if (minutes.Count > 0)
        {
            _minuteRepository.AddRange(minutes);
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