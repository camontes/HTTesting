using HR_Platform.Domain.DocumentManagementFileTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DocumentManagementFileTypeRepository(ApplicationDbContext context) : IDocumentManagementFileTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DocumentManagementFileType>> GetAll() => await _context.DocumentManagementFileTypes.AsNoTracking().Where(x => x.Name != "Ninguno").ToListAsync();
}