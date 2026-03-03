namespace HR_Platform.Domain.DefaultCollaboratorContracts;

public interface IDefaultCollaboratorContractRepository
{
    Task<List<DefaultCollaboratorContract>> GetAll();
}
