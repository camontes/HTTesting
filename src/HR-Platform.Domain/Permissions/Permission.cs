using HR_Platform.Domain.PermissionGroups;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.RolesPermissions;

namespace HR_Platform.Domain.Permissions;

public sealed class Permission : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Permission()
    {
    }

    public Permission(PermissionId id, PermissionGroupId permissionGroupId, string name, string nameEnglish, string description, string descriptionEnglish, string validationString)
    {
        Id = id;

        PermissionGroupId = permissionGroupId;

        Name = name;
        NameEnglish = nameEnglish;

        Description = description;
        DescriptionEnglish = descriptionEnglish;

        ValidationString = validationString;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public PermissionId Id { get; set; }

    public PermissionGroupId PermissionGroupId { get; set; }
    public PermissionGroup PermissionGroup { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string DescriptionEnglish { get; set; } = string.Empty;

    public string ValidationString { get; set; } = string.Empty;

    public List<RolePermission> RolesPermissions { get; set; }
}

