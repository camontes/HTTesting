namespace HR_Platform.Domain.DefaultFamilyCompositions;

public interface IDefaultFamilyCompositionRepository
{
    Task<List<DefaultFamilyComposition>> GetAll();
}
