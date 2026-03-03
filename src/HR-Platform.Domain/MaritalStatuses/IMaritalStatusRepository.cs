namespace HR_Platform.Domain.MaritalStatuses;

public interface IMaritalStatusRepository
{
    Task<List<MaritalStatus>> GetAll();
}
