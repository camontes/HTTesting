using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.ProfessionalAdvices;

public interface IProfessionalAdviceRepository
{
    Task<List<ProfessionalAdvice>> GetAll();
    Task<ProfessionalAdvice?> GetByIdAsync(ProfessionalAdviceId id);
    Task<ProfessionalAdvice?> GetNoneProfessionalAdviceByCompanyIdAsync(CompanyId companyId);
    Task<List<ProfessionalAdvice>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(ProfessionalAdviceId id);
    Task<int> GetNumberOfProfessionalAdvices(CompanyId id);
    void AddRangeProfessionalAdvices(List<ProfessionalAdvice> ProfessionalAdvice);
    void Add(ProfessionalAdvice pension);
    void Update(ProfessionalAdvice ProfessionalAdvice);
    void Delete(ProfessionalAdvice ProfessionalAdvice);
    void DeleteRange(List<ProfessionalAdvice> ProfessionalAdvices);
}
