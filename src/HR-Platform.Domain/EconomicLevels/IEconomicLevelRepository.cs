namespace HR_Platform.Domain.EconomicLevels;

public interface IEconomicLevelRepository
{
    Task<List<EconomicLevel>> GetAll();
}
