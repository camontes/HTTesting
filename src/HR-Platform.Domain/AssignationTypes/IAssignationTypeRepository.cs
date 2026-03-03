using HR_Platform.Domain.Assignations;

namespace HR_Platform.Domain.AssignationTypes;

public interface IAssignationTypeRepository
{
    Task<List<AssignationType>> GetAll();
    Task<bool> ExistsAsync(AssignationTypeId id);

}
