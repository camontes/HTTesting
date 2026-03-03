namespace HR_Platform.Domain.PriorityNovelties;

public interface IPriorityNoveltyRepository
{
    Task<List<PriorityNovelty>> GetAll();
}
