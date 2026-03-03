using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Delete;

public record DeleteEducationalLevelsCommand
(
    List<Guid> EducationalLevelsList,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

