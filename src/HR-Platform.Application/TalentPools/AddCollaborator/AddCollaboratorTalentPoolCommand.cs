using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.AddCollaborator;

public record AddCollaboratorTalentPoolCommand(List<Guid> TalentPoolIds, Guid CollaboratorId) : IRequest<ErrorOr<bool>>;