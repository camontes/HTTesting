using HR_Platform.Domain.HealthEntities;

namespace HR_Platform.Application.HealthEntities.Common;

public record HealthEntitiesAndCountByCompanyResponse
(
    List<HealthEntity> HealthEntities,

    int HealthEntitiesCount
);
