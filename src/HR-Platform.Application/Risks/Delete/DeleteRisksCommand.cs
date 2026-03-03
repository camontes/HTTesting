using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Risks.Delete;

public record DeleteRisksCommand
(
    Guid RiskId,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

