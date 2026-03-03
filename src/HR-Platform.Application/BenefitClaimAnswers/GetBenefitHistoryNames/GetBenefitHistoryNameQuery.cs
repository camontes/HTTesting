using ErrorOr;
using MediatR;

namespace BenefitClaimAnswers.GetBenefitHistoryNames;

public record GetBenefitHistoryNameQuery(Guid CompanyId) : IRequest<ErrorOr<List<string>>>;