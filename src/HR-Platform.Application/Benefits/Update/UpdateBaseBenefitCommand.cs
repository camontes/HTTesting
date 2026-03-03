using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Benefits.Update;

public record UpdateBaseBenefitCommand
(
    Guid BenefitId,
    string Name,
    string Description,
    bool IsAvailableForAll,
    int MinimumMonthsConstraint,
    bool IsAnotherContraint,
    string? AnotherContraint,
    IFormFile? File,
    bool IsChangedFile,
    IFormFile? Image,
    bool IsChangedImage,
    bool IsAddedButton,
    string? ButtonName,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;


