using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.InductionCompletedSearchFilter;

public class InductionsCompletedSearchFilterQuery(string query, int page, int pageSize, Guid companyId) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
    public Guid CompanyId = companyId;
}


