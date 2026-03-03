using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace HR_Platform.Application.TalentPools.TalentPoolSearchFilter;

public class TalentPoolSearchFilterQuery(string query, int page, int pageSize, bool? isTalentPoolArchived) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
    public bool? IsTalentPoolArchived = isTalentPoolArchived;
}
    
    