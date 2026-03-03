using ErrorOr;
using HR_Platform.Application.BenefitClaimAnswers.Common;
using MediatR;

namespace HR_Platform.Application.BenefitClaimAnswers.GetAllClaimsSent;

public record GetAllClaimsSentQuery(Guid CompanyId) : IRequest<ErrorOr<List<ClaimSentResponse>>>;