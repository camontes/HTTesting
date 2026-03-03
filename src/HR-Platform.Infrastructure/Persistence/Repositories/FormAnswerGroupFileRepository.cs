using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.FormAnswerGroupFiles;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FormAnswerGroupFileRepository(ApplicationDbContext context) : IFormAnswerGroupFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(FormAnswerGroupFile formAnswerGroupFile) => _context.FormAnswerGroupFiles.Add(formAnswerGroupFile);

        public void Delete(FormAnswerGroupFile formAnswerGroupFile) => _context.FormAnswerGroupFiles.Remove(formAnswerGroupFile);

        public void DeleteRange(List<FormAnswerGroupFile> formAnswerGroupFiles) => _context.FormAnswerGroupFiles.RemoveRange(formAnswerGroupFiles);

        public void Update(FormAnswerGroupFile formAnswerGroupFile) => _context.FormAnswerGroupFiles.Update(formAnswerGroupFile);

        public async Task<bool> ExistsAsync(FormAnswerGroupFileId id) => await _context.FormAnswerGroupFiles
            .AsNoTracking()
            .AnyAsync(fagf => fagf.Id == id);

        public async Task<List<FormAnswerGroupFile>> GetAll() => await _context.FormAnswerGroupFiles
            .AsNoTracking()
            .ToListAsync();

        public async Task<FormAnswerGroupFile?> GetByIdAsync(FormAnswerGroupFileId Id) =>
            await _context.FormAnswerGroupFiles
            .AsNoTracking()
            .SingleOrDefaultAsync(fag => fag.Id == Id);

        public void AddRange(List<FormAnswerGroupFile> formAnswerGroupFiles) => _context.FormAnswerGroupFiles.AddRange(formAnswerGroupFiles);
    }
}
