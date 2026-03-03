using HR_Platform.Domain.Companies;
using HR_Platform.Domain.NewCommunications;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NewCommunicationRepository(ApplicationDbContext context) : INewCommunicationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(NewCommunication NewCommunication) => _context.NewCommunications.Add(NewCommunication);
        public void AddRange(List<NewCommunication> NewCommunications) => _context.NewCommunications.AddRange(NewCommunications);

        public void Delete(NewCommunication NewCommunication) => _context.NewCommunications.Remove(NewCommunication);
        public void Update(NewCommunication NewCommunication) => _context.NewCommunications.Update(NewCommunication);

        public async Task<bool> ExistsAsync(NewCommunicationId id) => await _context.NewCommunications
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<NewCommunication?> GetByIdAsync(NewCommunicationId Id) =>
            await _context.NewCommunications
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<NewCommunication?> GetNoneNewCommunicationByCompanyIdAsync(CompanyId companyId) =>
            await _context.NewCommunications
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<NewCommunication>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.NewCommunications
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
    }
}
