using ErrorOr;
using HR_Platform.Application.Tags.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseTagsByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<TagsAndCountByCompanyResponse>>>;