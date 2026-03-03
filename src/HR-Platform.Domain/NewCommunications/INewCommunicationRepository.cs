using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.NewCommunications;

public interface INewCommunicationRepository
{
    Task<NewCommunication?> GetByIdAsync(NewCommunicationId id);
    Task<NewCommunication?> GetNoneNewCommunicationByCompanyIdAsync(CompanyId companyId);
    Task<List<NewCommunication>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(NewCommunicationId id);
    void Add(NewCommunication pension);
    void AddRange(List<NewCommunication> NewCommunications);
    void Update(NewCommunication NewCommunication);
    void Delete(NewCommunication NewCommunication);
}
