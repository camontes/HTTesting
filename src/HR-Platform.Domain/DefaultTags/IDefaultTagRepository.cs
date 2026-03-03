namespace HR_Platform.Domain.DefaultTags;

public interface IDefaultTagRepository
{
    Task<List<DefaultTag>> GetAll();
}
