using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.Create;

internal sealed class CreateEmergencyPlansCommandHandler(
    IEmergencyPlanRepository emergencyPlanRepository,
    IEmergencyPlanTypeRepository emergencyPlanTypeRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateEmergencyPlansCommand, ErrorOr<bool>>
{
    private readonly IEmergencyPlanRepository _emergencyPlanRepository = emergencyPlanRepository ?? throw new ArgumentNullException(nameof(emergencyPlanRepository));
    private readonly IEmergencyPlanTypeRepository _emergencyPlanTypeRepository = emergencyPlanTypeRepository ?? throw new ArgumentNullException(nameof(emergencyPlanTypeRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateEmergencyPlansCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("EmergencyPlans.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate ImageCreationDate)
            return Error.Validation("EmergencyPlans.ImageCreationDate", "Image Creation Date is not valid");

        if (await _emergencyPlanTypeRepository.GetByIdAsync(new EmergencyPlanTypeId(command.EmergencyPlanTypeId)) is not EmergencyPlanType emergencyPlan)
            return Error.Validation("EmergencyPlans.EmergencyPlan", "EmergencyPlanType with the provide Id was not found.");

        // 1- Pasarle el nombre que ya viene dado en el servicio de listado del tipo de plan y buscar el id por el nombre
        // 2- Crear un servicio por cada nombre y ya solo es asignarle el id pero igual tendria que buscarlo en la BD

        EmergencyPlan result = new
        (
            new EmergencyPlanId(Guid.NewGuid()),
            emergencyPlan.Id, // EmergencyPlanTypeId
            "", // Name -> This field doesn't exist in this section 
            command.Description is not null ? command.Description : string.Empty, // Description
            command.ImageFileURL is not null ? command.ImageFileURL : string.Empty, // ImageURL
            command.ImageFileName is not null ? command.ImageFileName : string.Empty, // ImageName
            ImageCreationDate, // ImageCreationTime
            command.VideoFileURL is not null ? command.VideoFileURL : string.Empty, // VideoURL
            command.VideoFileName is not null ? command.VideoFileName : string.Empty, // VideoName
            false, // IsVisible
            true,
            true,
            creationDate,
            creationDate
        );

        _emergencyPlanRepository.Add(result);

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