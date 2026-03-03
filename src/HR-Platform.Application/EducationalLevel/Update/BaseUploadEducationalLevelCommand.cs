using ErrorOr;
using HR_Platform.Application.EducationalLevels.Common;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Update;

public record BaseUpdateEducationalLevelCommand
(
    Guid Id,
    string Name

) : IRequest<ErrorOr<bool>>;
