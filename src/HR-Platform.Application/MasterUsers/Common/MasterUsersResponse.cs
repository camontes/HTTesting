namespace HR_Platform.Application.MasterUsers.Common;

public record MasterUsersResponse(
    Guid Id,

    string Email,

    string Name,
    string NameEnglish,

    string PhoneNumber,

    string Photo,
    string PhotoName,

    string RoleName,
    string RoleNameEnglish,

    string LoginCode
);
