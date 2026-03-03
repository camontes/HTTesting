using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DocumentManagements;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class DocumentManagementRepository(ApplicationDbContext context) : IDocumentManagementRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(DocumentManagement DocumentManagement) => _context.DocumentManagement.Add(DocumentManagement);
        public void AddRange(List<DocumentManagement> DocumentManagements) => _context.DocumentManagement.AddRange(DocumentManagements);

        public void Delete(DocumentManagement DocumentManagement) => _context.DocumentManagement.Remove(DocumentManagement);
        public void DeleteRange(List<DocumentManagement> DocumentManagements) => _context.DocumentManagement.RemoveRange(DocumentManagements);
        public void Update(DocumentManagement DocumentManagement) => _context.DocumentManagement.Update(DocumentManagement);

        public async Task<bool> ExistsAsync(DocumentManagementId id) => await _context.DocumentManagement
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<DocumentManagement?> GetByIdAsync(DocumentManagementId Id) =>
            await _context.DocumentManagement
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

       

        public async Task<List<DocumentManagement>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) => 
            await _context.DocumentManagement.Include(z => z.DocumentManagementFileType).Where(x => x.CollaboratorId == collaboratorId).ToListAsync();
    }
}
