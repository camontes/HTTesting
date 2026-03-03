using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Delete;

public record BaseDeleteEducationalLevelsCommand(List<Guid> EducationalLevelsList) : IRequest<ErrorOr<bool>>;

