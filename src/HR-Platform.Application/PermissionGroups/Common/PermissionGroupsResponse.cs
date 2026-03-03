namespace HR_Platform.Application.PermissionGroups.Common;

public record PermissionGroupsResponse(
    int Id,

    string Name,
    string NameEnglish,

    List<PermissionsResponse> Permissions
);
