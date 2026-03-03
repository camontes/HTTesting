using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Risks.Delete;

public record BaseDeleteRisksCommand(Guid RiskId) : IRequest<ErrorOr<bool>>;

