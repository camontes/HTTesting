using ErrorOr;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Delete;

public record DeleteSeveranceBenefitsCommand
(
    List<Guid> SeveranceBenefitsList,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

