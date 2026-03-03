namespace HR_Platform.Domain.DefaultPensions;

public interface IDefaultPensionRepository
{
    Task<List<DefaultPension>> GetAll();
}
