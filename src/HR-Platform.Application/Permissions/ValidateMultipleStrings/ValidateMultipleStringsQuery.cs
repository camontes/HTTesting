using MediatR;

namespace HR_Platform.Application.Permissions.ValidateMultipleStrings;

public record ValidateMultipleStringsQuery(List<string> PermissionStrings) : IRequest<bool>;