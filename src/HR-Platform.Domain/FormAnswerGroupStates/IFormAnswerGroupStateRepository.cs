namespace HR_Platform.Domain.FormAnswerGroupStates;

public interface IFormAnswerGroupStateRepository
{
    Task<List<FormAnswerGroupState>> GetAllWithoutNoneAsync();
}

