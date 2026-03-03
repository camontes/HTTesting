using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Benefits.Update;

public record UpdateBenefitsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    Guid BenefitId,
    string Name,
    string Description,
    bool IsAvailableForAll,
    int MinimumMonthsConstraint,
    bool IsAnotherContraint,
    string? AnotherContraint,
    bool IsChangedFile,
    string FileName,
    string FileURL,
    bool IsChangedImage,
    string ImageName,
    string ImageURL,
    bool IsAddedButton,
    string? ButtonName,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;



