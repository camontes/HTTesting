using ErrorOr;
using HR_Platform.Application.SeveranceBenefits.Common;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Update;

public record BaseUpdateSeveranceBenefitCommand
(
    Guid Id,
    string Name

) : IRequest<ErrorOr<bool>>;
