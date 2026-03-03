namespace HR_Platform.Application.PermissionGroups.Common;

public record PermissionsResponse(
    int Id,

    int PermissionGroupId,
    Guid RoleId,

    string Name,
    string NameEnglish,

    string Description,
    string DescriptionEnglish,

    bool IsEditable,
    bool IsCheck
);
