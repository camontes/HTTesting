using HR_Platform.Domain.CollaboratorDreamMapAnswers;

namespace HR_Platform.Domain.DreamMapAnswers;

public interface IDreamMapAnswerRepository 
{
    Task<List<DreamMapAnswer>> GetAll();
    Task<List<DreamMapAnswer>> GetAllCollaborators();
    Task<List<DreamMapAnswer>> GetAllCollaboratorsAnswers(CollaboratorDreamMapAnswerId collaboratorDreamMapAnswerId);
    Task<DreamMapAnswer?> GetByIdAsync(DreamMapAnswerId id);
    void AddRangeDreamMapAnswers(List<DreamMapAnswer> DreamMapAnswer);
    void Add(DreamMapAnswer pension);
    void Update(DreamMapAnswer DreamMapAnswer);
    public void UpdateRange(List<DreamMapAnswer> DreamMapAnswers);
    void Delete(DreamMapAnswer DreamMapAnswer);
    void DeleteRange(List<DreamMapAnswer> DreamMapAnswers);
}
