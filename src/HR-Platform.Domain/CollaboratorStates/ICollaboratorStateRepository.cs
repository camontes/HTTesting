namespace HR_Platform.Domain.CollaboratorStates;

public interface ICollaboratorStateRepository
{
    Task<List<CollaboratorState>> GetAll();
}
