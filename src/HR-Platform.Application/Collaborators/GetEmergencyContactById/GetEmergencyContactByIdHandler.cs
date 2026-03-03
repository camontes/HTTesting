using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.GetContractById;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.EmergencyContacts;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetById;

internal sealed class GetEmergencyContactByIdHandler(
    ICollaboratorRepository collaboratorRepository,
    IEmergencyContactRepository emergencyContactRepository
    ) : IRequestHandler<GetEmergencyContactByIdQuery, ErrorOr<EmergencyContactResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IEmergencyContactRepository _emergencyContactRepository = emergencyContactRepository ?? throw new ArgumentNullException(nameof(emergencyContactRepository));

    public async Task<ErrorOr<EmergencyContactResponse>> Handle(GetEmergencyContactByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(query.Id) is not Collaborator collaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        List<EmergencyContact> contactsResult = await _emergencyContactRepository.GetByCollaboratorIdAsync(collaborator.Id);

        List<UpdateEmergencyContactResponseFinal> responseList = [];

        foreach (EmergencyContact contact in contactsResult)
        {
            UpdateEmergencyContactResponseFinal values = new
            (
                contact.Id.Value,
                contact.CollaboratorId.Value,
                contact.ContactName,
                contact.PhoneNumber,
                contact.Relationship,
                contact.Address,
                contact.IsPrimaryContact
           );
            responseList.Add(values);
        }

        EmergencyContactResponse emergencyContactResponse = new (
            responseList,
            collaborator.EditionDate.Value
         );

        return emergencyContactResponse;
    }
}