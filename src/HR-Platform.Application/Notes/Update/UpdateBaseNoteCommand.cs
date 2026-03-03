using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Notes.Update;

public record UpdateBaseNoteCommand
(
    Guid Id,

    string Description,
    bool IsPublic,

    List<Guid>? NoteFilesIdsDelete,

    List<IFormFile>? NoteFiles
) : IRequest<ErrorOr<bool>>;


