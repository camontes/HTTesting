using ErrorOr;
using HR_Platform.Application.PermissionGroups.Common;
using MediatR;

namespace HR_Platform.Application.PermissionGroups.GetAll;

public record GetAllPermissionGroupsQuery(string RoleId) : IRequest<ErrorOr<IReadOnlyList<PermissionGroupsResponse>>>;