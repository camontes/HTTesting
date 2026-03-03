using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Roles.Create;

public record CreateRolesCommand(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
) : IRequest<ErrorOr<Guid>>;
