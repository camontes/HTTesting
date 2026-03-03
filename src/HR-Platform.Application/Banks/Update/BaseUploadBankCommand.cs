using ErrorOr;
using HR_Platform.Application.Banks.Common;
using MediatR;

namespace HR_Platform.Application.Banks.Update;

public record BaseUpdateBankCommand
(
    Guid Id,
    string Name

) : IRequest<ErrorOr<bool>>;
