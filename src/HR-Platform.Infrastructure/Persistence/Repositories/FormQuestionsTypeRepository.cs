using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FormQuestionsTypeRepository(ApplicationDbContext context) : IFormQuestionsTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public void Add(FormQuestionsType FormQuestionsType) => _context.FormQuestionsTypes.Add(FormQuestionsType);
        public void Delete(FormQuestionsType FormQuestionsType) => _context.FormQuestionsTypes.Remove(FormQuestionsType);
        public void Update(FormQuestionsType FormQuestionsType) => _context.FormQuestionsTypes.Update(FormQuestionsType);

        public async Task<FormQuestionsType?> GetByIdAsync(FormQuestionsTypeId Id) => await _context.FormQuestionsTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRange(List<FormQuestionsType> formQuestionsTypes) => _context.FormQuestionsTypes.AddRange(formQuestionsTypes);

        public async Task<FormQuestionsType?> GetByFormId(FormId formId) => 
            await _context.FormQuestionsTypes
            .AsNoTracking()
            .Include(x => x.Form)
            .ThenInclude(d => d.NoveltyType)
            .Include(t => t.QuestionType)
            .SingleOrDefaultAsync(r => r.FormId == formId);
    }
}
