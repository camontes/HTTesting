using HR_Platform.Domain.DocumentTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DocumentTypeRepository(ApplicationDbContext context) : IDocumentTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DocumentType>> GetAll() => await _context.DocumentTypes.AsNoTracking().ToListAsync();

    public async Task<DocumentType?> GetByIdAsync(DocumentTypeId value) => await _context.DocumentTypes.SingleOrDefaultAsync(x => x.Id == value);
    
}