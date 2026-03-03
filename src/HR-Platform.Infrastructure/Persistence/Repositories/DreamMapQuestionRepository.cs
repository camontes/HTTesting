using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DreamMapQuestions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class DreamMapQuestionRepository(ApplicationDbContext context) : IDreamMapQuestionRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(DreamMapQuestion DreamMapQuestion) => _context.DreamMapQuestions.Add(DreamMapQuestion);

        public void Delete(DreamMapQuestion DreamMapQuestion) => _context.DreamMapQuestions.Remove(DreamMapQuestion);
        public void DeleteRange(List<DreamMapQuestion> DreamMapQuestion) => _context.DreamMapQuestions.RemoveRange(DreamMapQuestion);
        public void Update(DreamMapQuestion DreamMapQuestion) => _context.DreamMapQuestions.Update(DreamMapQuestion);
        public void UpdateRange(List<DreamMapQuestion> DreamMapQuestions) => _context.DreamMapQuestions.UpdateRange(DreamMapQuestions);

        public async Task<List<DreamMapQuestion>> GetAll() => await _context.DreamMapQuestions
            .ToListAsync();

        public async Task<DreamMapQuestion?> GetByIdAsync(DreamMapQuestionId Id) =>
            await _context.DreamMapQuestions
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeDreamMapQuestions(List<DreamMapQuestion> DreamMapQuestion) => _context.DreamMapQuestions
            .AddRange(DreamMapQuestion);


        public async Task<List<DreamMapQuestion>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.DreamMapQuestions
            .Where(h => h.CompanyId == companyId)
            .ToListAsync();
    }
}
