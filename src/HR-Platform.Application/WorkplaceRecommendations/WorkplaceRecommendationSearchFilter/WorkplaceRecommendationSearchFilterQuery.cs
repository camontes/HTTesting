using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace HR_Platform.Application.WorkplaceRecommendations.WorkplaceRecommendationSearchFilter;

public class WorkplaceRecommendationSearchFilterQuery(string query, int page, int pageSize, string collaboratorEmail, string? collaboratorId, string? year) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
    public string CollaboratorEmail = collaboratorEmail;
    public string? CollaboratorId = collaboratorId;
    public string? Year = year;
}
    
    