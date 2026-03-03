namespace HR_Platform.Domain.DefaultKnowledgeLevels;

public interface IDefaultKnowledgeLevelRepository
{
    Task<List<DefaultKnowledgeLevel>> GetAll();
}
