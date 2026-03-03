using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.RemoveCollaboratorTalentPool;

public record RemoveCollaboratorTalentPoolQuery(Guid CollaboratorTalentPoolId) : IRequest<ErrorOr<bool>>;