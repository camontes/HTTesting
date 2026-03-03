namespace HR_Platform.Domain.DefaultPositions;

public interface IDefaultPositionRepository
{
    Task<List<DefaultPosition>> GetAll();
}
