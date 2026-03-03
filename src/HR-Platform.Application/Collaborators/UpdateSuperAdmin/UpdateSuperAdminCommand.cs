using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateSuperAdmin;
public record UpdateSuperAdminCommand(
    string EmailChangeBy,
    string Name,
    string Phone,
    bool IsChangedPhoto,
    string PhotoURL,
    string PhotoName

) : IRequest<ErrorOr<bool>>;
