using ErrorOr;
using HR_Platform.Application.HealthEntities.Common;
using MediatR;

namespace HR_Platform.Application.HealthEntities.GetByCompanyId;

public record GetHealthEntitiesByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<List<HealthEntitiesResponse>>>;