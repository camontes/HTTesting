using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace HR_Platform.Application.BrigadeDocumentations.BrigadeDocumentationSearchFilter;

public class BrigadeDocumentationSearchFilterQuery(string query, int page, int pageSize, string? year) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
    public string? Year = year;
}
    
    