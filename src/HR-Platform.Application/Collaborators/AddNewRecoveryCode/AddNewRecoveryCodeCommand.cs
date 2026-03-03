using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.AddNewRecoveryCode;

public record AddNewRecoveryCodeCommand(string Email) : IRequest<ErrorOr<RecoveryCodeResponse>>;