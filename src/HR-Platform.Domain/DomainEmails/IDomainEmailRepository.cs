using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.DomainEmails;

public interface IDomainEmailRepository
{
    Task<List<DomainEmail>> GetAll();
    Task<DomainEmail?> GetByIdAsync(DomainEmailId id);
    Task<List<DomainEmail>?> GetByCompanyIdAsync(CompanyId companyId);
    Task<bool> ExistsAsync(DomainEmailId id);
    Task<bool> ExistsDomainNameAsync(MailDomain domainEmail);
    void Add(DomainEmail domainEmail);
    void Update(DomainEmail domainEmail);
    void Delete(DomainEmail domainEmail);
}
