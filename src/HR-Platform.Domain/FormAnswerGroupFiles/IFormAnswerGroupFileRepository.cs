namespace HR_Platform.Domain.FormAnswerGroupFiles;

public interface IFormAnswerGroupFileRepository
{
    Task<List<FormAnswerGroupFile>> GetAll();
    Task<FormAnswerGroupFile?> GetByIdAsync(FormAnswerGroupFileId id);
    Task<bool> ExistsAsync(FormAnswerGroupFileId id);
    void AddRange(List<FormAnswerGroupFile> FormAnswerGroupFiles);
    void Add(FormAnswerGroupFile formAnswerGroupFile);
    void Update(FormAnswerGroupFile formAnswerGroupFile);
    void Delete(FormAnswerGroupFile formAnswerGroupFile);
    void DeleteRange(List<FormAnswerGroupFile> formAnswerGroupFile);
}
