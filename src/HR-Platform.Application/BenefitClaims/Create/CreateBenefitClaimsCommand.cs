using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.Create;

public record CreateBenefitClaimsCommand(Guid CompanyId, Guid BenefitId, string CollaboratorEmail) : IRequest<ErrorOr<bool>>;

