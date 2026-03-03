namespace HR_Platform.Domain.DefaultMonths;

public interface IDefaultMonthRepository
{
    Task<List<DefaultMonth>> GetAll();
}
