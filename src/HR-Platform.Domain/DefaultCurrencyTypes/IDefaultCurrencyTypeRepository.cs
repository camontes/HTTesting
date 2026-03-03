using HR_Platform.Domain.AssignationTypes;

namespace HR_Platform.Domain.DefaultCurrencyTypes;

public interface IDefaultCurrencyTypeRepository
{
    Task<List<DefaultCurrencyType>> GetAll();
    Task<bool> ExistsAsync(DefaultCurrencyTypeId id);

}
