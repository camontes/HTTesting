using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Notes.CreateAnswer;

public record CreateBaseNoteAnswerCommand
(
    string Description,
    Guid MainNoteId,
    List<IFormFile>? NoteAnswerFiles
) : IRequest<ErrorOr<bool>>;


