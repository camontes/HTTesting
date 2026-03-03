using ErrorOr;
using HR_Platform.Application.Notes.Common;
using MediatR;

namespace HR_Platform.Application.Notes.GetNoteForCollaborator;

public record GetNoteForCollaboratorQuery(string CollaboratorEmail, bool IsPublic ) : IRequest<ErrorOr<List<NotesResponse>>>;