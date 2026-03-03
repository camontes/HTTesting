using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Notes.Create;

public record CreateBaseNoteCommand
(
    string Description,
    Guid CollaboratorId,
    bool IsPublic,
    List<IFormFile>? NoteFiles
) : IRequest<ErrorOr<bool>>;


