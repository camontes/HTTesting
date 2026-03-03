namespace HR_Platform.Domain.DefaultLifePreferences;

public interface IDefaultLifePreferenceRepository
{
    Task<List<DefaultLifePreference>> GetAll();
}
