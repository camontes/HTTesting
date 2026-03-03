using HR_Platform.Domain.SurveyQuestions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class SurveyQuestionRepository(ApplicationDbContext context) : ISurveyQuestionRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(SurveyQuestion surveyQuestion) => _context.SurveyQuestions.Add(surveyQuestion);
        public void AddRange(List<SurveyQuestion> surveyQuestions) => _context.SurveyQuestions.AddRange(surveyQuestions);

        public void Delete(SurveyQuestion surveyQuestion) => _context.SurveyQuestions.Remove(surveyQuestion);
        public void Update(SurveyQuestion surveyQuestion) => _context.SurveyQuestions.Update(surveyQuestion);

        public async Task<bool> ExistsAsync(SurveyQuestionId id) => await _context.SurveyQuestions
            .AsNoTracking()
            .AnyAsync(sq => sq.Id == id);

        public async Task<SurveyQuestion?> GetByIdAsync(SurveyQuestionId Id) =>
            await _context.SurveyQuestions
            .AsNoTracking()
            .SingleOrDefaultAsync(sq => sq.Id == Id);
    }
}
