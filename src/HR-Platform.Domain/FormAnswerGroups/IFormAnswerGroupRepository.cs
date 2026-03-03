namespace HR_Platform.Domain.FormAnswerGroups;

public interface IFormAnswerGroupRepository
{
    Task<List<FormAnswerGroup>> GetAll();
    Task<FormAnswerGroup?> GetByIdAsync(FormAnswerGroupId id);
    Task<bool> ExistsAsync(FormAnswerGroupId id);
    void AddRange(List<FormAnswerGroup> formAnswerGroup);
    void Add(FormAnswerGroup formAnswerGroup);
    void Update(FormAnswerGroup formAnswerGroup);
    void Delete(FormAnswerGroup formAnswerGroup);
    void DeleteRange(List<FormAnswerGroup> formAnswerGroup);
}
