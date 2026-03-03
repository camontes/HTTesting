using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.OccupationalTests;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class OccupationalTestRepository(ApplicationDbContext context) : IOccupationalTestRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(OccupationalTest OccupationalTest) => _context.OccupationalTests.Add(OccupationalTest);
        public void Add(List<OccupationalTest> OccupationalTests) => _context.OccupationalTests.AddRange(OccupationalTests);

        public void Delete(OccupationalTest OccupationalTest) => _context.OccupationalTests.Remove(OccupationalTest);
        public void DeleteRange(List<OccupationalTest> OccupationalTests) => _context.OccupationalTests.RemoveRange(OccupationalTests);
        public void Update(OccupationalTest OccupationalTest) => _context.OccupationalTests.Update(OccupationalTest);

        public async Task<bool> ExistsAsync(OccupationalTestId id) => await _context.OccupationalTests
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<OccupationalTest?> GetByIdAsync(OccupationalTestId Id) =>
            await _context.OccupationalTests
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

       

        public async Task<List<OccupationalTest>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) => 
            await _context.OccupationalTests.Include(z => z.DefaultFileType).Where(x => x.CollaboratorId == collaboratorId).ToListAsync();
    }
}
