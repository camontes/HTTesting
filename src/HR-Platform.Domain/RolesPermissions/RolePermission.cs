using HR_Platform.Domain.Permissions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Roles;

namespace HR_Platform.Domain.RolesPermissions;

public sealed class RolePermission : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private RolePermission()
    {
    }

    public RolePermission(RolePermissionId id, PermissionId permissionId, RoleId roleId, bool isEditable, bool isCheck)
    {
        Id = id;

        PermissionId = permissionId;
        RoleId = roleId;

        IsEditable = isEditable;
        IsCheck = isCheck;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public RolePermissionId Id { get; set; }

    public PermissionId PermissionId { get; set; }
    public Permission Permission { get; set; }

    public RoleId RoleId { get; set; }
    public Role Role { get; set; }

    public bool IsEditable { get; set; }
    public bool IsCheck { get; set; }
}

