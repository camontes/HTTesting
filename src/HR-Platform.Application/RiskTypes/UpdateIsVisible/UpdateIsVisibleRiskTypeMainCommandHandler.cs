using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.RiskTypeMains;
using MediatR;

namespace HR_Platform.Application.Risks.UpdateIsVisible;

internal sealed class UpdateIsVisibleRiskTypeMainCommandHandler(
    IRiskTypeMainRepository riskTypeMainRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleRiskTypeMainCommand, ErrorOr<bool>>
{
    private readonly IRiskTypeMainRepository _riskTypeMainRepository = riskTypeMainRepository ?? throw new ArgumentNullException(nameof(riskTypeMainRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleRiskTypeMainCommand query, CancellationToken cancellationToken)
    {
        if (await _riskTypeMainRepository.GetByIdAsync(new RiskTypeMainId(query.Id)) is not RiskTypeMain oldRiskTypeMain)
            return Error.NotFound("Risk.NotFound", "The Risk with the provide Id was not found.");

        oldRiskTypeMain.IsVisible = !oldRiskTypeMain.IsVisible;
        _riskTypeMainRepository.Update(oldRiskTypeMain);

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