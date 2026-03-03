using HR_Platform.Domain.Companies;
using HR_Platform.Domain.OrganizationCharts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class OrganizationChartRepository(ApplicationDbContext context) : IOrganizationChartRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(OrganizationChart OrganizationChart) => _context.OrganizationCharts.Add(OrganizationChart);
        public void AddRange(List<OrganizationChart> OrganizationCharts) => _context.OrganizationCharts.AddRange(OrganizationCharts);

        public void Delete(OrganizationChart OrganizationChart) => _context.OrganizationCharts.Remove(OrganizationChart);
        public void Update(OrganizationChart OrganizationChart) => _context.OrganizationCharts.Update(OrganizationChart);

        public async Task<bool> ExistsAsync(OrganizationChartId id) => await _context.OrganizationCharts
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<OrganizationChart?> GetByIdAsync(OrganizationChartId Id) =>
            await _context.OrganizationCharts
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<OrganizationChart?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.OrganizationCharts
            .AsNoTracking()
            .SingleOrDefaultAsync(h => h.CompanyId == companyId);
        //.FirstOrDefaultAsync(h => h.CompanyId == companyId);

        public async Task<int> CountOrganizationChartAsync(CompanyId companyId) =>
            await _context.OrganizationCharts
            .AsNoTracking().Where(x => x.CompanyId == companyId).CountAsync();
    }
}
