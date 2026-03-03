using ErrorOr;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;

namespace SearchFilters.Query;

public record SearchFilterQuery
(
    string Query,
    int Page,
    int PageSize,
    string Context,
    string? Year,
    bool? IsPendingInvitation,
    bool? IsTalentPoolArchived,
    string? CollaboratorId,
    string? CompanyId,
    string? AreaId
) : IRequest<ErrorOr<SearchFilterResponse>>;