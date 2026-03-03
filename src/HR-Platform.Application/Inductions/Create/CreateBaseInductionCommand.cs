using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Inductions.Create;

public record CreateBaseInductionCommand
(
    string Name,
    string Description, 
    Guid CollaboratorId,
    List<IFormFile>? InductionFiles
) : IRequest<ErrorOr<bool>>;


