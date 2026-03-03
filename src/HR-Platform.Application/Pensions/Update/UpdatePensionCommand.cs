using ErrorOr;
using HR_Platform.Application.Pensions.Common;
using MediatR;

namespace HR_Platform.Application.Pensions.Update;

public record UpdatePensionCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<bool>>;
