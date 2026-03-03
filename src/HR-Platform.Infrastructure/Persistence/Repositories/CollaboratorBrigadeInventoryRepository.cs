using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorBrigadeInventoryRepository(ApplicationDbContext context) : ICollaboratorBrigadeInventoryRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorBrigadeInventory CollaboratorBrigadeInventory) => _context.CollaboratorBrigadeInventory.Add(CollaboratorBrigadeInventory);
        public void AddRange(List<CollaboratorBrigadeInventory> CollaboratorBrigadeInventories) => _context.CollaboratorBrigadeInventory.AddRange(CollaboratorBrigadeInventories);

        public void Delete(CollaboratorBrigadeInventory CollaboratorBrigadeInventory) => _context.CollaboratorBrigadeInventory.Remove(CollaboratorBrigadeInventory);
        public void Update(CollaboratorBrigadeInventory CollaboratorBrigadeInventory) => _context.CollaboratorBrigadeInventory.Update(CollaboratorBrigadeInventory);

        public async Task<bool> ExistsAsync(CollaboratorBrigadeInventoryId id) => await _context.CollaboratorBrigadeInventory
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<CollaboratorBrigadeInventory?> GetByIdAsync(CollaboratorBrigadeInventoryId Id) =>
            await _context.CollaboratorBrigadeInventory
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<CollaboratorBrigadeInventory?> GetNoneCollaboratorBrigadeInventoryByCompanyIdAsync(CompanyId companyId) =>
            await _context.CollaboratorBrigadeInventory
            .Where(p => p.CompanyId == companyId )
            .FirstOrDefaultAsync();
        
        public async Task<List<CollaboratorBrigadeInventory>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.CollaboratorBrigadeInventory.Include(x => x.UnitMeasure).Include(z => z.CollaboratorBrigadeInventoryFiles)
            .Include(y => y.CollaboratorBrigades)
                    .Where(h => h.CompanyId == companyId)
                    .ToListAsync();
    }
}
