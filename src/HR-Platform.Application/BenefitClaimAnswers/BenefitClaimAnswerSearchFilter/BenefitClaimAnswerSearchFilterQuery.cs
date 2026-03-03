using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace BenefitClaimAnswers.BenefitClaimAnswerSearchFilter;

public class BenefitClaimAnswerSearchFilterQuery(string query, int page, int pageSize) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
}

