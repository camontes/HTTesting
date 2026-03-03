namespace HR_Platform.Domain.DefaultStudyAreas;

public interface IDefaultStudyAreaRepository
{
    Task<List<DefaultStudyArea>> GetAll();
}
