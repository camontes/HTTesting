using ErrorOr;
using HR_Platform.Application.HealthEntities.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.HealthEntities;
using MediatR;

namespace HR_Platform.Application.HealthEntities.GetByCompanyId;

internal sealed class GetHealthEntitiesByCompanyIdHandler(
    IHealthEntityRepository healthEntityRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetHealthEntitiesByCompanyIdQuery, ErrorOr<List<HealthEntitiesResponse>>>
{
    private readonly IHealthEntityRepository _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<List<HealthEntitiesResponse>>> Handle(GetHealthEntitiesByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _healthEntityRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<HealthEntity> healthEntities)
        {
            return Error.NotFound("HealthEntities.NotFound", "The health entities related with the provide Company Id was not found.");
        }

        List<HealthEntitiesResponse> healthEntitiesResponse = [];

        if (healthEntities is not null && healthEntities.Count > 0)
        {
            foreach (HealthEntity healthEntity in healthEntities)
            {
                healthEntitiesResponse.Add
                (
                    new HealthEntitiesResponse
                    (
                        healthEntity.Id.Value,
                        healthEntity.CompanyId.Value,

                        healthEntity.Name,
                        healthEntity.NameEnglish,

                        healthEntity.Address.StreetAddress,
                        healthEntity.Address.CountryCode,
                        healthEntity.Address.Country,
                        healthEntity.Address.StateCode,
                        healthEntity.Address.State,
                        healthEntity.Address.CityCode,
                        healthEntity.Address.City,
                        healthEntity.Address.ZipCode,

                        healthEntity.Collaborators.Count,

                        healthEntity.IsEditable,
                        healthEntity.IsDeleteable,

                        healthEntity.CreationDate.Value,
                        healthEntity.EditionDate.Value
                    )
                );
            }
        }

        return healthEntitiesResponse;

    }
}