using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateEmergencyPlansCommand(
    Guid CompanyId,
    Guid EmergencyPlanTypeId,
    string Description,
    string? ImageFileName,
    string? ImageFileURL,
    string? VideoFileURL,
    string? VideoFileName
) : IRequest<ErrorOr<bool>>;



