namespace HR_Platform.Domain.DefaultProfessions;

public interface IDefaultProfessionRepository
{
    Task<List<DefaultProfession>> GetAll();
    Task<DefaultProfession?> GetOtheProfessionId();


}
