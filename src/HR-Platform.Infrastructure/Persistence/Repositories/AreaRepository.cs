using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class AreaRepository(ApplicationDbContext context) : IAreaRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<Area>> GetAll() => await _context.Areas.Where(a => a.Name != "Ninguno").AsNoTracking().ToListAsync();
    
    public async Task<List<Area>> GetByCompanyId(CompanyId companyId) => await _context.Areas.Where(a => a.Name != "Ninguno" && a.CompanyId == companyId)
        .AsNoTracking().ToListAsync();

    public async Task<Area?> GetByIdAsync(AreaId areaId) => await _context.Areas.SingleOrDefaultAsync(a => a.Id == areaId);

    public void Update(Area area) => _context.Areas.Update(area);
}