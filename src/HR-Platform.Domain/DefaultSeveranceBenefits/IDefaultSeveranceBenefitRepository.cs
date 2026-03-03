namespace HR_Platform.Domain.DefaultSeveranceBenefits;

public interface IDefaultSeveranceBenefitRepository
{
    Task<List<DefaultSeveranceBenefit>> GetAll();
}
