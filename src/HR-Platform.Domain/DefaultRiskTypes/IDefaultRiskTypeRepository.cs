namespace HR_Platform.Domain.DefaultRiskTypes;

public interface IDefaultRiskTypeRepository
{
    Task<List<DefaultRiskType>> GetAll();
}
