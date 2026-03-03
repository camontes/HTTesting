namespace HR_Platform.Domain.DefaultSoftSkills;

public interface IDefaultSoftSkillRepository
{
    Task<List<DefaultSoftSkill>> GetAll();
}
