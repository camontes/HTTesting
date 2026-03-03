using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BrigadeAdjustments;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BrigadeAdjustmentRepository(ApplicationDbContext context) : IBrigadeAdjustmentRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(BrigadeAdjustment BrigadeAdjustment) => _context.BrigadeAdjustments.Add(BrigadeAdjustment);

        public void Delete(BrigadeAdjustment BrigadeAdjustment) => _context.BrigadeAdjustments.Remove(BrigadeAdjustment);
        public void DeleteRange(List<BrigadeAdjustment> BrigadeAdjustments) => _context.BrigadeAdjustments.RemoveRange(BrigadeAdjustments);
        public void Update(BrigadeAdjustment BrigadeAdjustment) => _context.BrigadeAdjustments.Update(BrigadeAdjustment);

        public async Task<bool> ExistsAsync(BrigadeAdjustmentId id) => await _context.BrigadeAdjustments
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<BrigadeAdjustment>> GetAll() => await _context.BrigadeAdjustments
            .AsNoTracking()
            .ToListAsync();

        public async Task<BrigadeAdjustment?> GetByIdAsync(BrigadeAdjustmentId Id) =>
            await _context.BrigadeAdjustments
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<BrigadeAdjustment?> GetNoneBrigadeAdjustmentByCompanyIdAsync(CompanyId companyId) =>
            await _context.BrigadeAdjustments
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeBrigadeAdjustments(List<BrigadeAdjustment> BrigadeAdjustments) => _context.BrigadeAdjustments
            .AddRange(BrigadeAdjustments);

        public async Task<List<BrigadeAdjustment>?> GetByCompanyIdAsync(CompanyId companyId) =>
                await _context.BrigadeAdjustments
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
        public async Task<int> GetNumberOfBrigadeAdjustments(CompanyId id)
        {
            List<BrigadeAdjustment>? amount = await _context.BrigadeAdjustments.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count - 1;
        }
    }
}
