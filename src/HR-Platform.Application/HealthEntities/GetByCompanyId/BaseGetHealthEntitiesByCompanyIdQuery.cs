using ErrorOr;
using HR_Platform.Application.HealthEntities.Common;
using MediatR;

namespace HR_Platform.Application.HealthEntities.GetByCompanyId;

public record GetBaseHealthEntitiesByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<HealthEntitiesAndCountByCompanyResponse>>>;