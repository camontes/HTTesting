namespace HR_Platform.Domain.DefaultEducationalLevels;

public interface IDefaultEducationalLevelRepository
{
    Task<List<DefaultEducationalLevel>> GetAll();
}
