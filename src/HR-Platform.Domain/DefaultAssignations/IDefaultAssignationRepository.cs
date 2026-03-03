namespace HR_Platform.Domain.DefaultAssignations;

public interface IDefaultAssignationRepository
{
    Task<List<DefaultAssignation>> GetAll();
}
