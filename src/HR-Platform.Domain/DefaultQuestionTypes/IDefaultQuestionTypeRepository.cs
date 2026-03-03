namespace HR_Platform.Domain.DefaultQuestionTypes;

public interface IDefaultQuestionTypeRepository
{
    Task<List<DefaultQuestionType>> GetAll();
}
