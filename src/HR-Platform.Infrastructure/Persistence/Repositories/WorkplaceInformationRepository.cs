using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceInformations;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class WorkplaceInformationRepository(ApplicationDbContext context) : IWorkplaceInformationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(WorkplaceInformation WorkplaceInformation) => _context.WorkplaceInformations.Add(WorkplaceInformation);
        public void AddRange(List<WorkplaceInformation> WorkplaceInformations) => _context.WorkplaceInformations.AddRange(WorkplaceInformations);

        public void Delete(WorkplaceInformation WorkplaceInformation) => _context.WorkplaceInformations.Remove(WorkplaceInformation);
        public void DeleteRange(List<WorkplaceInformation> WorkplaceInformations) => _context.WorkplaceInformations.RemoveRange(WorkplaceInformations);
        public void Update(WorkplaceInformation WorkplaceInformation) => _context.WorkplaceInformations.Update(WorkplaceInformation);

        public async Task<bool> ExistsAsync(WorkplaceInformationId id) => await _context.WorkplaceInformations
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<WorkplaceInformation?> GetByIdAsync(WorkplaceInformationId Id) =>
            await _context.WorkplaceInformations
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

       

        public async Task<List<WorkplaceInformation>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) => 
            await _context.WorkplaceInformations.Where(x => x.CollaboratorId == collaboratorId).ToListAsync();
    }
}
