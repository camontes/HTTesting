using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.EmergencyContacts;

public interface IEmergencyContactRepository
{
    Task<List<EmergencyContact>> GetAll();
    Task<EmergencyContact?> GetByIdAsync(EmergencyContactId id);
    Task<List<EmergencyContact>> GetByCollaboratorIdAsync(CollaboratorId id);
    Task<int> GetNumberOfEmergencyContacts(CollaboratorId id);
    void AddRangeEmergencyContacts(List<EmergencyContact> EmergencyContact);
    void Add(EmergencyContact EmergencyContact);
    void Update(EmergencyContact EmergencyContact);
    void UpdateRangeAsync(List<EmergencyContact> EmergencyContact);
    void Delete(EmergencyContact EmergencyContact);
    void DeleteRangeEmergencyContacts(List<EmergencyContact> EmergencyContact);
}
