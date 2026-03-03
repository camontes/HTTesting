using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.GetActiveCollaboratorByInductionId;

public record GetActiveCollaboratorByInductionIdQuery(Guid InductionId) : IRequest<ErrorOr<List<CollaboratorActiveResponse>>>;