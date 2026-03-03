using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.OrganizationCharts;

public interface IOrganizationChartRepository
{
    Task<OrganizationChart?> GetByIdAsync(OrganizationChartId id);
    Task<OrganizationChart?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<int> CountOrganizationChartAsync(CompanyId companyId);
    Task<bool> ExistsAsync(OrganizationChartId id);
    void Add(OrganizationChart pension);
    void AddRange(List<OrganizationChart> OrganizationCharts);
    void Update(OrganizationChart OrganizationChart);
    void Delete(OrganizationChart OrganizationChart);
}
