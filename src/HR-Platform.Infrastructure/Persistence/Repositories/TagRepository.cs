using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Tags;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class TagRepository(ApplicationDbContext context) : ITagRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Tag Tag) => _context.Tag.Add(Tag);

        public void Delete(Tag Tag) => _context.Tag.Remove(Tag);
        
        public void DeleteRange(List<Tag> tags) => _context.Tag.RemoveRange(tags);

        public void Update(Tag Tag) => _context.Tag.Update(Tag);

        public async Task<bool> ExistsAsync(TagId id) => await _context.Tag
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<Tag>> GetAll() => await _context.Tag
            .AsNoTracking()
            .ToListAsync();

        public async Task<Tag?> GetByIdAsync(TagId Id) =>
            await _context.Tag
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Tag?> GetNoneTagByCompanyIdAsync(CompanyId companyId) =>
            await _context.Tag
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeTag(List<Tag> Tag) => _context.Tag
            .AddRange(Tag);

        public async Task<List<Tag>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.Tag.Include(c => c.CollaboratorTags)
                    .Where(t => t.CompanyId == companyId )
                    .ToListAsync();
            }
            else
            {
                return await _context.Tag.Include(c => c.CollaboratorTags)
                    .Where(t => t.CompanyId == companyId)
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }


        public async Task<int> GetNumberOfTags(CompanyId id) {
           List<Tag>? amount =  await _context.Tag.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }

        public void AddRangeTags(List<Tag> Tags) => _context.Tag.AddRange(Tags);
        
    }
}
