using ErrorOr;
using HR_Platform.Application.TalentPools.Common;
using MediatR;

namespace HR_Platform.Application.TalentPools.GetByCollaboratorId;

public record GetTalentPoolsByCollaboratorIdQuery(Guid CollaboratorId) : IRequest<ErrorOr<List<TalentPoolByCollaboratorIdResponse>>>;