using HR_Platform.Domain.Forms;

namespace HR_Platform.Domain.FormQuestionsTypes;
public interface IFormQuestionsTypeRepository
{
    Task<FormQuestionsType?> GetByIdAsync(FormQuestionsTypeId id);
    Task<FormQuestionsType?> GetByFormId(FormId formId);
    void AddRange(List<FormQuestionsType> formQuestionsType);
    void Add(FormQuestionsType formQuestionsType);
    void Update(FormQuestionsType formQuestionsType);
    void Delete(FormQuestionsType formQuestionsType);
}
