using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Notes.Update;

public record UpdateNoteCommand
(
    Guid Id,

    string Description,
    bool IsPublic,

    string EmailChangeBy,

    List<UpdateNotesObjectCommand> NotesList,

    List<Guid>? NoteFilesIdsDelete,

    List<IFormFile>? NoteFiles
) : IRequest<ErrorOr<bool>>;

public record UpdateNotesObjectCommand(
    IFormFile NoteFile,
    string FileName,
    string UrlFile
);
