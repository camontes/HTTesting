using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Minutes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Minutes.Create;

internal sealed class CreateMinutesCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IMinuteRepository MinuteRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateMinutesCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IMinuteRepository _minuteRepository = MinuteRepository ?? throw new ArgumentNullException(nameof(MinuteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateMinutesCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Minutes.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<Minute> minutes = [];

        foreach (CreateMinutesObjectCommand item in command.MinutesList)
        {
            Minute result = new
            (
                new MinuteId(Guid.NewGuid()),
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