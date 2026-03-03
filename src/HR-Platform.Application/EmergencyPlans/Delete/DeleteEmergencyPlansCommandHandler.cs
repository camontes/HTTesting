using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.Delete;

internal sealed class DeleteEmergencyPlansCommandHandler(
    IEmergencyPlanRepository emergencyPlanRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteEmergencyPlansCommand, ErrorOr<bool>>
{
    private readonly IEmergencyPlanRepository _emergencyPlanRepository = emergencyPlanRepository ?? throw new ArgumentNullException(nameof(emergencyPlanRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteEmergencyPlansCommand query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (await _emergencyPlanRepository.GetByIdAsync(new EmergencyPlanId(query.EmergencyPlanId)) is not EmergencyPlan emergencyPlan)
            return Error.NotFound("EmergencyPlan.NotFound", "The Emergency Plan with the provide Id was not found.");

        try
        {
            _emergencyPlanRepository.Delete(emergencyPlan);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}