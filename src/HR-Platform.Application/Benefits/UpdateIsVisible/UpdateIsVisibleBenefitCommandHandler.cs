using ErrorOr;
using HR_Platform.Application.Benefits.UpdateIsVisible;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Benefits;
using MediatR;

namespace HR_Platform.Application.Benefits.UpdateIsVisibleBenefit;

internal sealed class UpdateIsVisibleBenefitCommandHandler(
    IBenefitRepository benefitRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleBenefitCommand, ErrorOr<bool>>
{
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleBenefitCommand query, CancellationToken cancellationToken)
    {
        if (await _benefitRepository.GetByIdAsync(new BenefitId(query.Id)) is not Benefit oldBenefit)
        {
            return Error.NotFound("Benefit.NotFound", "The Benefit with the provide Id was not found.");
        }

        oldBenefit.IsVisible = !oldBenefit.IsVisible;
        _benefitRepository.Update(oldBenefit);

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