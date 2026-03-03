namespace HR_Platform.Domain.DefaultDaysOfWeeks;

public interface IDefaultDaysOfWeekRepository
{
    Task<List<DefaultDaysOfWeek>> GetAll();
}
