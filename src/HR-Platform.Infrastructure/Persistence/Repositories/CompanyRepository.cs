using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CompanyRepository(ApplicationDbContext context) : ICompanyRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(Company company) => _context.Companies.Add(company);
    public void Delete(Company company) => _context.Companies.Remove(company);
    public void Update(Company company) => _context.Companies.Update(company);
    public async Task<bool> ExistsAsync(CompanyId id) => await _context.Companies.AsNoTracking().AnyAsync(c => c.Id == id);
    public async Task<Company?> GetByIdAsync(CompanyId id) => await _context.Companies.AsNoTracking().Include(c => c.Collaborators).SingleOrDefaultAsync(c => c.Id == id);
    public async Task<Company?> GetByEmailAsync(string email) => await _context.Companies.AsNoTracking().SingleOrDefaultAsync(c => c.Email == Email.Create(email));
    public async Task<List<Company>> GetAll() => await _context.Companies.AsNoTracking().ToListAsync();
}