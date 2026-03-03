using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TalentPools.UpdateArchivedStateById;

public record UpdateArchivedStateByIdQuery(Guid TalentPoolId) : IRequest<ErrorOr<bool>>;