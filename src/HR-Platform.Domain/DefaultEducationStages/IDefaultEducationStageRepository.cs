namespace HR_Platform.Domain.DefaultEducationStages;

public interface IDefaultEducationStageRepository
{
    Task<List<DefaultEducationStage>> GetAll();
}
