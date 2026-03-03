using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Update;

public record UpdateEmergencyPlansCommand(
    Guid CompanyId,
    Guid EmergencyPlanId,
    Guid EmergencyPlanTypeId,
    string Description,
    bool IsUpdateImageFile,
    bool IsUpdateVideoFile,
    string? ImageFileName,
    string? ImageFileURL,
    string? VideoFileURL,
    string? VideoFileName
) : IRequest<ErrorOr<bool>>;



