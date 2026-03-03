using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Tags;

public interface ITagRepository
{
    Task<List<Tag>> GetAll();
    Task<Tag?> GetByIdAsync(TagId id);
    Task<Tag?> GetNoneTagByCompanyIdAsync(CompanyId companyId);
    Task<List<Tag>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(TagId id);
    Task<int> GetNumberOfTags(CompanyId id);
    void AddRangeTags(List<Tag> Tag);
    void Add(Tag Tag);
    void Update(Tag Tag);
    void Delete(Tag Tag);
    void DeleteRange(List<Tag> tags);
}
