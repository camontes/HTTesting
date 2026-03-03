using ErrorOr;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Delete;

public record BaseDeleteHealthEntitiesCommand(List<Guid> HealthEntitiesList) : IRequest<ErrorOr<bool>>;

