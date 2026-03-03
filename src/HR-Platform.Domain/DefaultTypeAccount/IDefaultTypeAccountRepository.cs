namespace HR_Platform.Domain.DefaultTypeAccounts;

public interface IDefaultTypeAccountRepository
{
    Task<List<DefaultTypeAccount>> GetAll();
}
