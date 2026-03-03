using ErrorOr;
using HR_Platform.Application.Notes.Common;
using MediatR;

namespace HR_Platform.Application.Notes.GetByCollaboratorId;

public record GetBaseNoteByCollaboratorIdQuery(Guid CollaboratorId, bool IsPublic) : IRequest<ErrorOr<List<NotesResponse>>>;