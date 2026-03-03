using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateBenefitsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    string Name,
    string Description,
    bool IsAvailableForAll,
    int MinimumMonthsConstraint,
    bool IsAnotherContraint,
    string? AnotherContraint,
    string? FileName,
    string? FileURL,
    string? ImageName,
    string? ImageURL,
    bool IsAddedButton,
    string? ButtonName,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;



