using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DomainEmailRepository(ApplicationDbContext context) : IDomainEmailRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(DomainEmail domainEmail) => _context.DomainEmails.Add(domainEmail);
    public void Delete(DomainEmail domainEmail) => _context.DomainEmails.Remove(domainEmail);
    public void Update(DomainEmail domainEmail) => _context.DomainEmails.Update(domainEmail);
    public async Task<bool> ExistsAsync(DomainEmailId id) => await _context.DomainEmails.AsNoTracking().AnyAsync(c => c.Id == id);
    public async Task<bool> ExistsDomainNameAsync(MailDomain domainEmail) => await _context.DomainEmails.AsNoTracking().AnyAsync(c => c.Domain == domainEmail);
    public async Task<DomainEmail?> GetByIdAsync(DomainEmailId id) => await _context.DomainEmails.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<DomainEmail>?> GetByCompanyIdAsync(CompanyId companyId) => await _context.DomainEmails.AsNoTracking().Where(c => c.CompanyId == companyId).ToListAsync();
    public async Task<List<DomainEmail>> GetAll() => await _context.DomainEmails.AsNoTracking().ToListAsync();
}