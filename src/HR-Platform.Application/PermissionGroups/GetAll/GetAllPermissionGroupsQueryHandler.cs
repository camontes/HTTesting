using ErrorOr;
using MediatR;
using HR_Platform.Application.PermissionGroups.GetAll;
using HR_Platform.Application.PermissionGroups.Common;
using HR_Platform.Domain.PermissionGroups;
using HR_Platform.Domain.RolesPermissions;
using HR_Platform.Domain.Permissions;

namespace HR_Platform.Application.PermisssionGroups.GetAll;
internal sealed class GetAllPermisssionGroupsQueryHandler(
    IPermissionGroupRepository permissionGroupRepository,
    IRolePermissionRepository rolePermissionRepository
    ) : IRequestHandler<GetAllPermissionGroupsQuery, ErrorOr<IReadOnlyList<PermissionGroupsResponse>>>
{
    private readonly IPermissionGroupRepository _permissionGroupRepository = permissionGroupRepository ?? throw new ArgumentNullException(nameof(permissionGroupRepository));
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository ?? throw new ArgumentNullException(nameof(rolePermissionRepository));

    public async Task<ErrorOr<IReadOnlyList<PermissionGroupsResponse>>> Handle(GetAllPermissionGroupsQuery query, CancellationToken cancellationToken)
    {
        IList<PermissionGroup> permissionGroups = await _permissionGroupRepository.GetAll();

        List<PermissionGroupsResponse> permissionGroupsResponse = [];

        if (permissionGroups is not null && permissionGroups.Count > 0)
        {
            foreach (PermissionGroup permissionGroup in permissionGroups)
            {
                PermissionGroupsResponse permissionGroupResponse = new
                (
                    permissionGroup.Id.Value,

                    permissionGroup.Name,
                    permissionGroup.NameEnglish,

                    []
                );

                if (permissionGroup.Permissions is not null && permissionGroup.Permissions.Count > 0)
                {
                    foreach (Permission permission in permissionGroup.Permissions)
                    {
                        List<RolePermission> rolePermissions = await _rolePermissionRepository.GetByRoleAndPermissionIdAsync(new(Guid.Parse(query.RoleId)), permission.Id.Value);

                        if (rolePermissions is not null && rolePermissions.Count > 0)
                        {
                            foreach (RolePermission rolePermission in rolePermissions)
                            {
                                PermissionsResponse permissionsResponse = new
                                (
                                    permission.Id.Value,

                                    permission.PermissionGroupId.Value,

                                    rolePermission.RoleId.Value,

                                    permission.Name,
                                    permission.NameEnglish,

                                    permission.Description,
                                    permission.DescriptionEnglish,

                                    rolePermission.IsEditable,
                                    rolePermission.IsCheck
                                );

                                permissionGroupResponse.Permissions.Add(permissionsResponse);

                                break;
                            }
                        }
                    }
                }

                permissionGroupsResponse.Add(permissionGroupResponse);
            }
        }

        return permissionGroupsResponse;
    }
}