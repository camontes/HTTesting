using ErrorOr;
using MediatR;

namespace HR_Platform.Application.MasterUsers.Update;

public record UpdateMasterUserCommand
(
    string EmailChangeBy,
    string Name,
    string Phone,
    bool IsChangedPhoto,
    string PhotoURL,
    string PhotoName
) : IRequest<ErrorOr<bool>>;
