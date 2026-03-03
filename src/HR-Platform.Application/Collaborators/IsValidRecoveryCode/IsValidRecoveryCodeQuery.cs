using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Collaborators.IsValidRecoveryCode;
public record IsValidRecoveryCodeQuery(Email Email, string RecoveryCode) : IRequest<ErrorOr<BooleanExistsResponse>>;
