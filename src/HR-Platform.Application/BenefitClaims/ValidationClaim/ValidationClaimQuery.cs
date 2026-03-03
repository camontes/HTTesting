using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.ValidationClaim;

public record ValidationClaimQuery(Guid BenefitId, string CollaboratorEmail) : IRequest<ErrorOr<bool>>;