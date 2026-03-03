using ErrorOr;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.Companies;
using MediatR;

namespace BenefitClaimAnswers.GetBenefitHistoryNames;

internal sealed class GetBenefitHistoryNameQueryHandler(
    IBenefitClaimAnswerRepository benefitClaimAnswerRepository

    ) : IRequestHandler<GetBenefitHistoryNameQuery, ErrorOr<List<string>>>
{
    private readonly IBenefitClaimAnswerRepository _benefitClaimAnswerRepository = benefitClaimAnswerRepository ?? throw new ArgumentNullException(nameof(benefitClaimAnswerRepository));

    public async Task<ErrorOr<List<string>>> Handle(GetBenefitHistoryNameQuery query, CancellationToken cancellationToken)
    {
        List<BenefitClaimAnswer>? benefitClaimAnswer = await _benefitClaimAnswerRepository.GetBenefitNamesAsync(new CompanyId(query.CompanyId));
        List<string> result = [];
        if (benefitClaimAnswer is not null && benefitClaimAnswer.Count > 0)
        {
            var uniqueNames = benefitClaimAnswer
             .GroupBy(e => e.BenefitName)
             .Select(g => new { BenefitName = g.Key, BenefitClaimAnswerId = g.First().Id })
             .ToList();

            foreach (var benefit in uniqueNames)
            {
                result.Add(benefit.BenefitName);
            }
        }
        return result;
    }
}