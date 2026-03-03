using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BirthdayTemplateSettings;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class BirthdayTemplateSettingRepository(ApplicationDbContext context) : IBirthdayTemplateSettingRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(BirthdayTemplateSetting role) => _context.BirthdayTemplateSettings.Add(role);
    public void Delete(BirthdayTemplateSetting role) => _context.BirthdayTemplateSettings.Remove(role);
    public void Update(BirthdayTemplateSetting role) => _context.BirthdayTemplateSettings.Update(role);
    public async Task<bool> ExistsAsync(BirthdayTemplateSettingId id) => await _context.BirthdayTemplateSettings.AsNoTracking().AnyAsync(r => r.Id == id);
    public async Task<BirthdayTemplateSetting?> GetByIdAsync(BirthdayTemplateSettingId id) => await _context.BirthdayTemplateSettings.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
    public async Task<BirthdayTemplateSetting?> GetByCompanyIdAsync(CompanyId companyId) => await _context.BirthdayTemplateSettings.SingleOrDefaultAsync(r => r.CompanyId == companyId);
  
    public async Task<List<BirthdayTemplateSetting>> GetAll() => await _context.BirthdayTemplateSettings.AsNoTracking().ToListAsync();


}