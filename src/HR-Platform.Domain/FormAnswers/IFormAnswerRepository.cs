using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.FormAnswers;

public interface IFormAnswerRepository
{
    Task<List<FormAnswer>> GetAll();
    Task<FormAnswer?> GetByIdAsync(FormAnswerId id);
    Task<SearchFilter<FormAnswer>> GetByCollaboratorIdAndNameSearchAsync(NoveltiesRequestSearch request);
    Task<SearchFilter<FormAnswer>> GetByCompanyIdAndNameSearchAsync(NoveltiesRequestSearch request);
    Task<bool> ExistsAsync(FormAnswerId id);
    void AddRange(List<FormAnswer> FormAnswer);
    void Add(FormAnswer FormAnswer);
    void Update(FormAnswer FormAnswer);
    void Delete(FormAnswer FormAnswer);
    void DeleteRange(List<FormAnswer> FormAnswers);
}
