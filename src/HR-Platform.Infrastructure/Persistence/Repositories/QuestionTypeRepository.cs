using HR_Platform.Domain.Companies;
using HR_Platform.Domain.QuestionTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class QuestionTypeRepository(ApplicationDbContext context) : IQuestionTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(QuestionType QuestionType) => _context.QuestionTypes.Add(QuestionType);

        public void Delete(QuestionType QuestionType) => _context.QuestionTypes.Remove(QuestionType);

        public void DeleteRange(List<QuestionType> tags) => _context.QuestionTypes.RemoveRange(tags);

        public void Update(QuestionType QuestionType) => _context.QuestionTypes.Update(QuestionType);

        public async Task<bool> ExistsAsync(QuestionTypeId id) => await _context.QuestionTypes
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<QuestionType>> GetAll() => await _context.QuestionTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<QuestionType?> GetByIdAsync(QuestionTypeId Id) =>
            await _context.QuestionTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<QuestionType?> GetNoneQuestionTypeByCompanyIdAsync(CompanyId companyId) =>
            await _context.QuestionTypes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeQuestionType(List<QuestionType> QuestionType) => _context.QuestionTypes
            .AddRange(QuestionType);

        public async Task<List<QuestionType>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.QuestionTypes
                    .Where(t => t.CompanyId == companyId && t.Name != "Ninguno")
                    .ToListAsync();

        public async Task<int> GetNumberOfQuestionTypes(CompanyId id)
        {
            List<QuestionType>? amount = await _context.QuestionTypes.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count - 1;
        }

        public void AddRangeQuestionTypes(List<QuestionType> QuestionTypes) => _context.QuestionTypes.AddRange(QuestionTypes);

    }
}
