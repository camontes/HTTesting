using ErrorOr;
using MediatR;

namespace HR_Platform.Application.PensionEntities.Create;

public record CreateBasePensionsCommand(List<BasePensionEntityCommand> PensionEntitiesList) : IRequest<ErrorOr<bool>>;

public record BasePensionEntityCommand(
    string Name,
    string NameEnglish
);

