namespace HR_Platform.Domain.DefaultProfessionalAdvices;

public interface IDefaultProfessionalAdviceRepository
{
    Task<List<DefaultProfessionalAdvice>> GetAll();
}
