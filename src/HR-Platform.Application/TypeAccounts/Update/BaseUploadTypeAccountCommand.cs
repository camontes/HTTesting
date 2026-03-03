using ErrorOr;
using HR_Platform.Application.TypeAccounts.Common;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.Update;

public record BaseUpdateTypeAccountCommand
(
    Guid Id,
    string Name
) : IRequest<ErrorOr<bool>>;
