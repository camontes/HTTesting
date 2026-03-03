using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BenefitClaimAnswers.Create;

public record CreateBaseBenefitClaimAnswersCommand
(
    Guid BenefitClaimId,
    bool IsBenefitAccepeted,
    string Details
)
:
IRequest<ErrorOr<bool>>;

