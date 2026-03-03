using ErrorOr;
using HR_Platform.Application.EmergencyPlans.Common;
using HR_Platform.Application.EmergencyPlans.GetById;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.EmergencyPlanTypes;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.GetByCollaboratorId;

internal sealed class GetEmergencyPlanByIdQueryHandler(
    IEmergencyPlanTypeRepository emergencyPlanTypeRepository,
    IEmergencyPlanRepository emergencyPlanRepository,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetEmergencyPlanByIdQuery, ErrorOr<List<EmergencyPlanAllContentResponse>>>
{
    private readonly IEmergencyPlanTypeRepository _emergencyPlanTypeRepository = emergencyPlanTypeRepository ?? throw new ArgumentNullException(nameof(emergencyPlanTypeRepository));
    private readonly IEmergencyPlanRepository _emergencyPlanRepository = emergencyPlanRepository ?? throw new ArgumentNullException(nameof(emergencyPlanRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<EmergencyPlanAllContentResponse>>> Handle(GetEmergencyPlanByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _emergencyPlanTypeRepository.GetByIdAsync(new EmergencyPlanTypeId(query.Id)) is not EmergencyPlanType oldEmergencyPlanType)
        {
            return Error.NotFound("EmergencyPlanType.NotFound", "The Emergency Plan Type with the provide Id was not found.");
        }


        List<EmergencyPlan>? emergencyPlans = await _emergencyPlanRepository.GetEmergencyPlanTypeId(oldEmergencyPlanType.Id);
        List<EmergencyPlanAllContentResponse> resultList = [];

        if (emergencyPlans is not null)
        {
            foreach (var item in emergencyPlans)
            {
                EmergencyPlanAllContentResponse response = new
                (
                    item.Id.Value.ToString(),
                    item.Description,
                    item.ImageName,
                    item.ImageURL,
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.ImageCreationTime.Value).Split('.')[0]), // TimePosted
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.ImageCreationTime.Value).Split('.')[1]), // TimePostedEnglish
                    item.VideoName,
                    item.VideoURL,
                    item.IsVisible
                );
                resultList.Add(response);
            }
        }

        return resultList;

    }
}