using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EducationalLevelEntities.Create;

public record CreateBaseEducationalLevelsCommand(List<BaseEducationalLevelEntityCommand> EducationalLevelEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseEducationalLevelEntityCommand(
    string Name,
    string NameEnglish
);

