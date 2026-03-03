using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.ValidateLoginCode;

public record ValidateLoginCodeCommand(string LoginCode, string Email) : IRequest<ErrorOr<ValidateLoginCodeResponse>>;