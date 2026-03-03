using ErrorOr;
using HR_Platform.Application.EducationalLevels.Common;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Update;

public record UpdateEducationalLevelCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<bool>>;
