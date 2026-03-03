using ErrorOr;
using HR_Platform.Application.SeveranceBenefits.Common;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Update;

public record UpdateSeveranceBenefitCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<bool>>;
