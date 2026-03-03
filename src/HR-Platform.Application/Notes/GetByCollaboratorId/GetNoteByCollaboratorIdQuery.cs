using ErrorOr;
using HR_Platform.Application.Notes.Common;
using MediatR;

namespace HR_Platform.Application.Notes.GetByCollaboratorId;

public record GetNoteByCollaboratorIdQuery(string EmailWhoLogIn, Guid CollaboratorId, bool IsPublic) : IRequest<ErrorOr<List<NotesResponse>>>;