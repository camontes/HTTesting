using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.OrganizationCharts;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.OrganizationCharts.Create;

internal sealed class CreateOrganizationChartsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IOrganizationChartRepository OrganizationChartRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateOrganizationChartsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IOrganizationChartRepository _organizationChartRepository = OrganizationChartRepository ?? throw new ArgumentNullException(nameof(OrganizationChartRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateOrganizationChartsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("OrganizationCharts.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        int countOC = await _organizationChartRepository.CountOrganizationChartAsync(new CompanyId(command.CompanyId));

        if (countOC > 1)
            return Error.Validation("OrganizationCharts.countOC", "An organizational chart already exists");

        OrganizationChart organizationChart = new
        (
            new OrganizationChartId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),
            command.IsByFile,
            command.IsByUrl,
            command.FileName is not null ? command.FileName: string.Empty,
            command.FileURL,
            creationDate, //FileCreatedDate
            command.EmailChangeBy,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
            true,
            true,
            creationDate,
            creationDate
        );

        try
        {
            _organizationChartRepository.Add(organizationChart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}