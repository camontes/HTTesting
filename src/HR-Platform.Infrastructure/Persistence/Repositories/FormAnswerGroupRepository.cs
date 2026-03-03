using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.FormAnswerGroups;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FormAnswerGroupRepository(ApplicationDbContext context) : IFormAnswerGroupRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(FormAnswerGroup formAnswerGroup) => _context.FormAnswerGroups.Add(formAnswerGroup);

        public void Delete(FormAnswerGroup formAnswerGroup) => _context.FormAnswerGroups.Remove(formAnswerGroup);

        public void DeleteRange(List<FormAnswerGroup> formAnswerGroups) => _context.FormAnswerGroups.RemoveRange(formAnswerGroups);

        public void Update(FormAnswerGroup formAnswerGroup) => _context.FormAnswerGroups.Update(formAnswerGroup);

        public async Task<bool> ExistsAsync(FormAnswerGroupId id) => await _context.FormAnswerGroups
            .AsNoTracking()
            .AnyAsync(fag => fag.Id == id);

        public async Task<List<FormAnswerGroup>> GetAll() => await _context.FormAnswerGroups
            .AsNoTracking()
            .ToListAsync();

        public async Task<FormAnswerGroup?> GetByIdAsync(FormAnswerGroupId Id) =>
            await _context.FormAnswerGroups
            .AsNoTracking()
            .SingleOrDefaultAsync(fag => fag.Id == Id);

        public void AddRange(List<FormAnswerGroup> formAnswerGroups) => _context.FormAnswerGroups.AddRange(formAnswerGroups);
    }
}
