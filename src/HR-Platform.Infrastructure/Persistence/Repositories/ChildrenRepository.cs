using HR_Platform.Domain.ChildrenNamespace;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ChildrenRepository(ApplicationDbContext context) : IChildrenRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Children children) => _context.Children.Add(children);

        public void Delete(Children children) => _context.Children.Remove(children);
        public void Update(Children children) => _context.Children.Update(children);

        public async Task<Children?> GetByIdAsync(ChildrenId Id) =>
            await _context.Children
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<Children>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.Children
            .Where(p => p.CollaboratorId == collaboratorId)
            .ToListAsync();
    }
}
