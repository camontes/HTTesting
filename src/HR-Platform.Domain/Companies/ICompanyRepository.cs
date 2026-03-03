namespace HR_Platform.Domain.Companies;

public interface ICompanyRepository
{
    Task<List<Company>> GetAll();
    Task<Company?> GetByIdAsync(CompanyId id);
    Task<Company?> GetByEmailAsync(string email);
    Task<bool> ExistsAsync(CompanyId id);
    void Add(Company company);
    void Update(Company company);
    void Delete(Company company);
}
