namespace HR_Platform.Domain.DefaultBanks;

public interface IDefaultBankRepository
{
    Task<List<DefaultBank>> GetAll();
}
