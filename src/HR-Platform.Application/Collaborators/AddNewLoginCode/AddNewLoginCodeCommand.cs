using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.AddNewLoginCode;

public record AddNewLoginCodeCommand(string Email) : IRequest<ErrorOr<LoginCodeResponse>>;