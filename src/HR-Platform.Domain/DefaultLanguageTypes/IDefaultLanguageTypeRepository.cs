namespace HR_Platform.Domain.DefaultLanguageTypes;

public interface IDefaultLanguageTypeRepository
{
    Task<List<DefaultLanguageType>> GetAll();
}
