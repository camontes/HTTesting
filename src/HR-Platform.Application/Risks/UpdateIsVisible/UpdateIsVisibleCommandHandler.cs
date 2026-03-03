using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Risks;
using MediatR;

namespace HR_Platform.Application.Risks.UpdateIsVisible;

internal sealed class UpdateIsVisibleCommandHandler(
    IRiskRepository riskRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleCommand, ErrorOr<bool>>
{
    private readonly IRiskRepository _riskRepository = riskRepository ?? throw new ArgumentNullException(nameof(riskRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleCommand query, CancellationToken cancellationToken)
    {
        if (await _riskRepository.GetByIdAsync(new RiskId(query.Id)) is not Risk oldRisk)
        {
            return Error.NotFound("Risk.NotFound", "The Risk with the provide Id was not found.");
        }

        oldRisk.IsVisible = !oldRisk.IsVisible;
        _riskRepository.Update(oldRisk);

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