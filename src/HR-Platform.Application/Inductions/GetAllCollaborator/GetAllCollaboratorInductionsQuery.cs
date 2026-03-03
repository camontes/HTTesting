using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.GetAllCollaborator;

public record GetAllCollaboratorInductionsQuery(Guid InductionId) : IRequest<ErrorOr<List<CollaboratorListResponse>>>;