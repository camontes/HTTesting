using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Forms;

public interface IFormRepository
{
    Task<List<Form>> GetAll();
    Task<Form?> GetByIdAsync(FormId id);
    Task<Form?> GetByIdWithoutIncludesAsync(FormId Id);
    Task<Form?> GetNoneFormByCompanyIdAsync(CompanyId companyId);
    Task<List<Form>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<List<Form>?> GetByAreaIdAsync(AreaId areaId);
    Task<bool> ExistsAsync(FormId id);
    Task<int> GetNumberOfForms(CompanyId id);
    void AddRangeForms(List<Form> Form);
    void Add(Form Form);
    void Update(Form Form);
    void Delete(Form Form);
    void DeleteRange(List<Form> Forms);
}
