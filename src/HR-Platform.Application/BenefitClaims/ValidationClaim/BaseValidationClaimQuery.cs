using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.ValidationClaim;

public record BaseValidationClaimQuery(Guid BenefitId) : IRequest<ErrorOr<bool>>;