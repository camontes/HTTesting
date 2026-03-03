using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateNewCommunicationsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    string Name,
    string Description,
    string? FileName,
    string? FileURL,
    string? ImageName,
    string? ImageURL,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;



