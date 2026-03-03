using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BenefitClaimAnswers.Create;

public record CreateBenefitClaimAnswersCommand
(
    Guid BenefitClaimId,
    bool IsBenefitAccepeted,
    string Details,
    string CollaboratorWhoManagedEmail

):IRequest<ErrorOr<bool>>;

