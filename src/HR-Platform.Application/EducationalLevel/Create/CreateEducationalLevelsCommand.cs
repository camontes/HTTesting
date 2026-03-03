using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Create;

public record CreateEducationalLevelsCommand(List<EducationalLevelData> EducationalLevelsDataList) : IRequest<ErrorOr<bool>>;

public record EducationalLevelData(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
);

