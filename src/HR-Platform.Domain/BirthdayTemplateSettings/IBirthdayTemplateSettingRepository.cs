using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.BirthdayTemplateSettings;

public interface IBirthdayTemplateSettingRepository
{
    Task<List<BirthdayTemplateSetting>> GetAll();
    Task<BirthdayTemplateSetting?> GetByIdAsync(BirthdayTemplateSettingId id);
    Task<BirthdayTemplateSetting?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(BirthdayTemplateSettingId id);
    void Add(BirthdayTemplateSetting role);
    void Update(BirthdayTemplateSetting role);
    void Delete(BirthdayTemplateSetting role);
}
