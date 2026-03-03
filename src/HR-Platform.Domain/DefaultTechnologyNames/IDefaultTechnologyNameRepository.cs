namespace HR_Platform.Domain.DefaultTechnologyNames;

public interface IDefaultTechnologyNameRepository
{
    Task<List<DefaultTechnologyName>> GetAll();
}
