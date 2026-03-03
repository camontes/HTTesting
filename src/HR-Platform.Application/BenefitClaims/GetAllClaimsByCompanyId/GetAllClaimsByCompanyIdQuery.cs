using ErrorOr;
using HR_Platform.Application.BenefitClaims.Common;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.GetAllClaimsByCompanyId;

public record GetAllClaimsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<BenefitClaimsResponse>>>;