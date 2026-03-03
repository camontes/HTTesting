using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FormRepository(ApplicationDbContext context) : IFormRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Form Form) => _context.Forms.Add(Form);

        public void Delete(Form Form) => _context.Forms.Remove(Form);

        public void DeleteRange(List<Form> tags) => _context.Forms.RemoveRange(tags);

        public void Update(Form Form) => _context.Forms.Update(Form);

        public async Task<bool> ExistsAsync(FormId id) => await _context.Forms
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<Form>> GetAll() => await _context.Forms
            .AsNoTracking()
            .ToListAsync();

        public async Task<Form?> GetByIdAsync(FormId Id) =>
            await _context.Forms
            .AsNoTracking()
            .Include(x => x.NoveltyType)
            .Include(x => x.FormQuestionsTypes)
            .ThenInclude(x => x.QuestionType)
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Form?> GetByIdWithoutIncludesAsync(FormId Id) =>
            await _context.Forms
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Form?> GetNoneFormByCompanyIdAsync(CompanyId companyId) =>
            await _context.Forms
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeForm(List<Form> Form) => _context.Forms
            .AddRange(Form);

        public async Task<List<Form>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.Forms
                    .Where(t => t.CompanyId == companyId)
                    .ToListAsync();

        public async Task<int> GetNumberOfForms(CompanyId id)
        {
            List<Form>? amount = await _context.Forms.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count - 1;
        }

        public void AddRangeForms(List<Form> Forms) => _context.Forms.AddRange(Forms);

        public async Task<List<Form>?> GetByAreaIdAsync(AreaId areaId) =>
            await _context.Forms.Where(p => p.NoveltyTypeId == areaId).ToListAsync();
    }
}
