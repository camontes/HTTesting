namespace HR_Platform.Domain.DefaultContractTypes;

public interface IDefaultContractTypeRepository
{
    Task<List<DefaultContractType>> GetAll();
}
