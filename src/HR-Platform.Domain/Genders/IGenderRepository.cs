namespace HR_Platform.Domain.Genders;

public interface IGenderRepository
{
    Task<List<Gender>> GetAll();
}
