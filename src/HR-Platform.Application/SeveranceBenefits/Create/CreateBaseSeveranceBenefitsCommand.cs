using ErrorOr;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefitEntities.Create;

public record CreateBaseSeveranceBenefitsCommand(List<BaseSeveranceBenefitEntityCommand> SeveranceBenefitEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseSeveranceBenefitEntityCommand(
    string Name,
    string NameEnglish
);

