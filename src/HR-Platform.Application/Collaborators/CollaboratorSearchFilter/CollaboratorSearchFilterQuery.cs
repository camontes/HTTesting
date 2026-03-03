using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;
namespace HR_Platform.Application.Collaborators.CollaboratorSearchFilter;

public class CollaboratorSearchFilterQuery(string query, int page, int pageSize, bool? isPendingInvitation) : SearchFilterQueryBase, IRequest<ErrorOr<SearchFilterResponse>>
{
    public string Query = query;
    public int Page = page;
    public int PageSize = pageSize;
    public bool? IsPendingInvitation = isPendingInvitation;
}


