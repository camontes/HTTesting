using ErrorOr;
using HR_Platform.Application.Notes.Common;
using MediatR;

namespace HR_Platform.Application.Notes.GetNoteForCollaborator;

public record GetBaseNoteForCollaboratorQuery(bool IsPublic) : IRequest<ErrorOr<List<NotesResponse>>>;