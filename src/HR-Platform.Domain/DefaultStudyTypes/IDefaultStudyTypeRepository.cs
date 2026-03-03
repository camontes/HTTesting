namespace HR_Platform.Domain.DefaultStudyTypes;

public interface IDefaultStudyTypeRepository
{
    Task<List<DefaultStudyType>> GetAll();
}
