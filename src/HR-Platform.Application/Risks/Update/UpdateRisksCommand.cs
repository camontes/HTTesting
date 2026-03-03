using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Update;

public record UpdateRisksCommand(
    Guid CompanyId,
    Guid RiskId,
    string Name,
    string? Description,
    bool IsUpdateImageFile,
    bool IsUpdateVideoFile,
    string? ImageFileName,
    string? ImageFileURL,
    string? VideoFileURL,
    string? VideoFileName
) : IRequest<ErrorOr<bool>>;



