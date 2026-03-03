using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Benefits.Create;

public record CreateBaseBenefitCommand
(
    string Name,
    string Description,
    bool IsAvailableForAll,
    int MinimumMonthsConstraint,
    bool IsAnotherContraint,
    string? AnotherContraint,
    IFormFile? File,
    IFormFile? Image,
    bool IsAddedButton,
    string? ButtonName,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;


