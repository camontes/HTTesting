using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.Create;

internal sealed class CreateCoexistenceCommitteeMinutesCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    ICoexistenceCommitteeMinuteRepository CoexistenceCommitteeMinuteRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCoexistenceCommitteeMinutesCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ICoexistenceCommitteeMinuteRepository _coexistenceCommitteeMinuteRepository = CoexistenceCommitteeMinuteRepository ?? throw new ArgumentNullException(nameof(CoexistenceCommitteeMinuteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateCoexistenceCommitteeMinutesCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("CoexistenceCommitteeMinutes.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<CoexistenceCommitteeMinute> coexistenceCommitteeMinutes = [];

        foreach (CreateCoexistenceCommitteeMinutesObjectCommand item in command.CoexistenceCommitteeMinutesList)
        {
            CoexistenceCommitteeMinute result = new
            (
                new CoexistenceCommitteeMinuteId(Guid.NewGuid()),
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
            coexistenceCommitteeMinutes.Add(result);
        }

        if (coexistenceCommitteeMinutes.Count > 0)
        {
            _coexistenceCommitteeMinuteRepository.AddRange(coexistenceCommitteeMinutes);
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