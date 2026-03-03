using ErrorOr;
using HR_Platform.Application.BenefitClaims.Common;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.GetAllClaimsByCompanyId;

public record GetAllClaimsByCollaboratorIdQuery(Guid BenefitClaimId) : IRequest<ErrorOr<CollaboratorBenefitClaimsResponse>>;