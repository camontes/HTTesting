using ErrorOr;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Delete;

public record BaseDeleteSeveranceBenefitsCommand(List<Guid> SeveranceBenefitsList) : IRequest<ErrorOr<bool>>;

