using ErrorOr;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.UpdateIsVisible;

internal sealed class UpdateIsVisibleEmergencyPlanCommandHandler(
    IEmergencyPlanTypeRepository emergencyPlanTypeRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleEmergencyPlanCommand, ErrorOr<bool>>
{
    private readonly IEmergencyPlanTypeRepository _emergencyPlanTypeRepository = emergencyPlanTypeRepository ?? throw new ArgumentNullException(nameof(emergencyPlanTypeRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleEmergencyPlanCommand query, CancellationToken cancellationToken)
    {
        if (await _emergencyPlanTypeRepository.GetByIdAsync(new EmergencyPlanTypeId(query.Id)) is not EmergencyPlanType oldEmergencyPlanType)
            return Error.NotFound("EmergencyPlan.NotFound", "The Emergency Plan with the provide Id was not found.");

        oldEmergencyPlanType.IsVisible = !oldEmergencyPlanType.IsVisible;
        _emergencyPlanTypeRepository.Update(oldEmergencyPlanType);

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