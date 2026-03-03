using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.FamilyCompositions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FamilyCompositionRepository(ApplicationDbContext context) : IFamilyCompositionRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(FamilyComposition familyComposition) => _context.FamilyCompositions.Add(familyComposition);

        public void Delete(FamilyComposition familyComposition) => _context.FamilyCompositions.Remove(familyComposition);
        public void Update(FamilyComposition familyComposition) => _context.FamilyCompositions.Update(familyComposition);

        public async Task<FamilyComposition?> GetByIdAsync(FamilyCompositionId Id) =>
            await _context.FamilyCompositions
            .AsNoTracking()
            .SingleOrDefaultAsync(f => f.Id == Id);

        public async Task<List<FamilyComposition>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.FamilyCompositions
            .Where(p => p.CollaboratorId == collaboratorId)
            .ToListAsync();
    }
}
