using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace HR_Platform.Application.DreamMapAnswers.DreamMapAnswerSearchFilter;

public class DreamMapAnswerSearchFilterQuery(string query, int page, int pageSize) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
}
    
    