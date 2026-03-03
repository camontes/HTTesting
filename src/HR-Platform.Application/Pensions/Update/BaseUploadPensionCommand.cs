using ErrorOr;
using HR_Platform.Application.Pensions.Common;
using MediatR;

namespace HR_Platform.Application.Pensions.Update;

public record BaseUpdatePensionCommand
(
    Guid Id,
    string Name

) : IRequest<ErrorOr<bool>>;
