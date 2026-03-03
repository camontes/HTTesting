namespace HR_Platform.Domain.BloodTypes;

public interface IBloodTypeRepository
{
    Task<List<BloodType>> GetAll();
}
