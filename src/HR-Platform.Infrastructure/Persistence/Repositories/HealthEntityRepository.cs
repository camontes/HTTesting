using HR_Platform.Domain.Companies;
using HR_Platform.Domain.HealthEntities;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class HealthEntityRepository(ApplicationDbContext context) : IHealthEntityRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(HealthEntity healthEntity) => _context.HealthEntities.Add(healthEntity);

        public void Delete(HealthEntity healthEntity) => _context.HealthEntities.Remove(healthEntity);
        public void DeleteRange(List<HealthEntity> healthEntities) => _context.HealthEntities.RemoveRange(healthEntities);

        public void Update(HealthEntity healthEntity) => _context.HealthEntities.Update(healthEntity);

        public async Task<bool> ExistsAsync(HealthEntityId id) => await _context.HealthEntities
            .AsNoTracking()
            .AnyAsync(h => h.Id == id);

        public async Task<List<HealthEntity>> GetAll() => await _context.HealthEntities
            .AsNoTracking()
            .ToListAsync();

        public async Task<HealthEntity?> GetByIdAsync(HealthEntityId Id) =>
            await _context.HealthEntities
            .AsNoTracking()
            .SingleOrDefaultAsync(h => h.Id == Id);

        public async Task<HealthEntity?> GetNoneHealthEntityByCompanyIdAsync(CompanyId companyId) =>
            await _context.HealthEntities
            .Where(h => h.CompanyId == companyId && h.Name == "Ninguno")
            .FirstOrDefaultAsync();

        public void AddRange(List<HealthEntity> healthEntities) => _context.HealthEntities
            .AddRange(healthEntities);

        public async Task<List<HealthEntity>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.HealthEntities.Include(c => c.Collaborators)
                    .Include(x => x.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.HealthEntities.Include(c => c.Collaborators)
                    .Include(x => x.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }
    }
}
