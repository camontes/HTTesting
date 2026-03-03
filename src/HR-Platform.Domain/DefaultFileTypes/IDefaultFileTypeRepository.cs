namespace HR_Platform.Domain.DefaultFileTypes;

public interface IDefaultFileTypeRepository
{
    Task<List<DefaultFileType>> GetAll();
}
