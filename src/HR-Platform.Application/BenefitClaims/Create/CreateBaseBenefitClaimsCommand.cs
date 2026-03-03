using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BenefitClaimEntities.Create;

public record CreateBaseBenefitClaimsCommand(Guid BenefitId) : IRequest<ErrorOr<bool>>;


