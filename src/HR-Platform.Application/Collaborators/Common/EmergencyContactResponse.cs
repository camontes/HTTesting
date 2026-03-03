namespace HR_Platform.Application.Collaborators.Common;
public record EmergencyContactResponse
(
    List<UpdateEmergencyContactResponseFinal> EmergencyContacts,
    DateTime EditionDate
);

public record UpdateEmergencyContactResponseFinal 
(
    Guid Id,
    Guid CollaboratorId,
    string ContactName,
    string PhoneNumber,
    string Relationship,
    string Address,
    bool IsPrimaryContact
);
