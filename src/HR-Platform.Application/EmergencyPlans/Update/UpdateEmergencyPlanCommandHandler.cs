using ErrorOr;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.Update;

internal sealed class UpdateEmergencyPlansCommandHandler(
    IEmergencyPlanRepository emergencyPlanRepository,
    IEmergencyPlanTypeRepository emergencyPlanTypeRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateEmergencyPlansCommand, ErrorOr<bool>>
{
    private readonly IEmergencyPlanRepository _emergencyPlanRepository = emergencyPlanRepository ?? throw new ArgumentNullException(nameof(emergencyPlanRepository));
    private readonly IEmergencyPlanTypeRepository _emergencyPlanTypeRepository = emergencyPlanTypeRepository ?? throw new ArgumentNullException(nameof(emergencyPlanTypeRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateEmergencyPlansCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");
        int changesConter = 0;

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("EmergencyPlans.EditionDate", "EditionDate is not valid");

        if (await _emergencyPlanRepository.GetByIdAsync(new EmergencyPlanId(command.EmergencyPlanId)) is not EmergencyPlan emergencyPlan)
            return Error.Validation("EmergencyPlans.EmergencyPlan", "Emergency Plan with the provide Id was not found.");

        if (await _emergencyPlanTypeRepository.GetByIdAsync(new EmergencyPlanTypeId(command.EmergencyPlanTypeId)) is null)
            return Error.Validation("EmergencyPlanTypes.EmergencyPlanType", "Emergency Plan Type with the provide Id was not found.");

        if (!string.IsNullOrEmpty(command.Description))
        {
            emergencyPlan.Description = command.Description;
            changesConter += 1;
        }

        if (emergencyPlan.ImageURL != command.ImageFileURL && command.IsUpdateImageFile)
        {
            emergencyPlan.ImageURL = command.ImageFileURL is not null ? command.ImageFileURL : string.Empty;
            emergencyPlan.ImageCreationTime = editionDate;
            changesConter += 1;
        }

        if (emergencyPlan.ImageName != command.ImageFileName && command.IsUpdateImageFile)
        {
            emergencyPlan.ImageName = command.ImageFileName is not null ? command.ImageFileName : string.Empty;
            emergencyPlan.ImageCreationTime = editionDate;
            changesConter += 1;
        }

        if (emergencyPlan.VideoURL != command.VideoFileURL && command.IsUpdateVideoFile)
        {
            emergencyPlan.VideoURL = command.VideoFileURL is not null ? command.VideoFileURL : string.Empty;
            changesConter += 1;
        }

        if (emergencyPlan.VideoName != command.VideoFileName && command.IsUpdateVideoFile)
        {
            emergencyPlan.VideoName = command.VideoFileName is not null ? command.VideoFileName : string.Empty;
            changesConter += 1;
        }

        try
        {
            if(changesConter > 0)
            {
                emergencyPlan.EditionDate = editionDate;
                _emergencyPlanRepository.Update(emergencyPlan);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}