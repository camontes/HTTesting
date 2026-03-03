using HR_Platform.Domain.UnitMeasures;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class UnitMeasureRepository(ApplicationDbContext context) : IUnitMeasureRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(UnitMeasure UnitMeasure) => _context.UnitMeasures.Add(UnitMeasure);

        public void Delete(UnitMeasure UnitMeasure) => _context.UnitMeasures.Remove(UnitMeasure);
        
        public void DeleteRange(List<UnitMeasure> tags) => _context.UnitMeasures.RemoveRange(tags);

        public void Update(UnitMeasure UnitMeasure) => _context.UnitMeasures.Update(UnitMeasure);

        public async Task<bool> ExistsAsync(UnitMeasureId id) => await _context.UnitMeasures
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<UnitMeasure>> GetAll() => await _context.UnitMeasures
            .Where(p => p.Name != "Ninguno")
            .AsNoTracking()
            .ToListAsync();

        public async Task<UnitMeasure?> GetByIdAsync(UnitMeasureId Id) =>
            await _context.UnitMeasures
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<UnitMeasure?> GetNoneUnitMeasureByCompanyIdAsync() =>
            await _context.UnitMeasures
            .Where(p => p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeUnitMeasure(List<UnitMeasure> UnitMeasure) => _context.UnitMeasures
            .AddRange(UnitMeasure);


        public async Task<int> GetNumberOfUnitMeasures() {
           List<UnitMeasure>? amount =  await _context.UnitMeasures.ToListAsync();
           return amount.Count - 1;
        }

        public void AddRangeUnitMeasures(List<UnitMeasure> UnitMeasures) => _context.UnitMeasures.AddRange(UnitMeasures);
    }
}
