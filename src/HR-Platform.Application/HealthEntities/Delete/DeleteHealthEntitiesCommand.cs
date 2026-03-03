using ErrorOr;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Delete;

public record DeleteHealthEntitiesCommand
(
    List<Guid> HealthEntitiesList,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

