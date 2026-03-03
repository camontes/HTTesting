using ErrorOr;
using MediatR;
using HR_Platform.Application.Tags.Common;

namespace HR_Platform.Application.Tags.GetByCompanyId;

public record GetTagsByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<TagsAndCountByCompanyResponse>>;