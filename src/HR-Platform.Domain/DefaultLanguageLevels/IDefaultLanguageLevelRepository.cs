namespace HR_Platform.Domain.DefaultLanguageLevels;

public interface IDefaultLanguageLevelRepository
{
    Task<List<DefaultLanguageLevel>> GetAll();
}
