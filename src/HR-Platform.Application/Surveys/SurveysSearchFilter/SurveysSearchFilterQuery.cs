using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace HR_Platform.Application.Surveys.SurveysSearchFilter;

public class SurveysSearchFilterQuery(string query, int page, int pageSize, Guid companyId, string areaId)
:
SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
    public Guid CompanyId = companyId;
    public Guid AreaId = Guid.Parse(areaId);
}