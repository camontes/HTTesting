using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Inductions.Update;

public record UpdateBaseInductionCommand
(
    Guid InductionId,
    string Name,
    string Description, 
    bool HasChangedFiles,
    List<Guid>? FileNamesDeleted,
    List<IFormFile>? InductionFiles
) : IRequest<ErrorOr<bool>>;


