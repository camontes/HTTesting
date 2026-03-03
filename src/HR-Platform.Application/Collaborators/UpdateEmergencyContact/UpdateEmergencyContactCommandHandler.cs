using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.UpdateEducationData;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.EmergencyContacts;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateEmergencyContactCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IEmergencyContactRepository emergencyContactRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateBaseEmergencyContactCommand, ErrorOr<EmergencyContactResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IEmergencyContactRepository _emergencyContactRepository = emergencyContactRepository ?? throw new ArgumentNullException(nameof(emergencyContactRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<EmergencyContactResponse>> Handle(UpdateBaseEmergencyContactCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");


        Collaborator? oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.Parse(command.CollaboratorId)));
    
        if(oldCollaborator is null)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");


        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);


        List<EmergencyContact> emergencyContactsListExisting = await _emergencyContactRepository.GetByCollaboratorIdAsync(new CollaboratorId(Guid.Parse(command.CollaboratorId)));
        List<EmergencyContact> updatedEmergencyContactsList = [];
        List<EmergencyContact> AddEmergencyContactsList = [];
        

        if (command is not null && command.EmergencyContactsList is not null && command.EmergencyContactsList.Count > 0 && command.EmergencyContactsList.Count < 3)
        {
            foreach (UpdateEmergencyContactCommand emergencyContactCommand in command.EmergencyContactsList)
            {
                EmergencyContactId emergencyContactId = string.IsNullOrEmpty(emergencyContactCommand.Id)
                ? new EmergencyContactId(Guid.NewGuid())
                : new EmergencyContactId(Guid.Parse(emergencyContactCommand.Id));

                EmergencyContact? existingEmergencyContact = emergencyContactsListExisting.FirstOrDefault(ec => ec.Id == emergencyContactId);

                if (existingEmergencyContact is not null)
                {
                    // Si existe, verificar si ha habido cambios en los datos
                    if (existingEmergencyContact.ContactName != emergencyContactCommand.ContactName ||
                        existingEmergencyContact.PhoneNumber != emergencyContactCommand.PhoneNumber ||
                        existingEmergencyContact.Relationship != emergencyContactCommand.Relationship ||
                        existingEmergencyContact.Address != emergencyContactCommand.Address ||
                        existingEmergencyContact.IsPrimaryContact != emergencyContactCommand.IsPrimaryContact)
                    {
                        // Actualizar los datos del contacto de emergencia existente
                        existingEmergencyContact.ContactName = !string.IsNullOrEmpty(emergencyContactCommand.ContactName) ? emergencyContactCommand.ContactName : string.Empty;
                        existingEmergencyContact.PhoneNumber = !string.IsNullOrEmpty(emergencyContactCommand.PhoneNumber) ? emergencyContactCommand.PhoneNumber : string.Empty;
                        existingEmergencyContact.Relationship = !string.IsNullOrEmpty(emergencyContactCommand.Relationship) ? emergencyContactCommand.Relationship : string.Empty;
                        existingEmergencyContact.Address = !string.IsNullOrEmpty(emergencyContactCommand.Address) ? emergencyContactCommand.Address : string.Empty;
                        existingEmergencyContact.IsPrimaryContact = emergencyContactCommand.IsPrimaryContact;
                        existingEmergencyContact.EditionDate = editionDate;
                    }
                    updatedEmergencyContactsList.Add(existingEmergencyContact);
                }
                else if (existingEmergencyContact is null && emergencyContactsListExisting.Count < 2)
                {
                    // Si no existe, crear un nuevo contacto de emergencia
                    EmergencyContact newEmergencyContact = new(
                        emergencyContactId,
                        new CollaboratorId(Guid.Parse(command.CollaboratorId)),
                        !string.IsNullOrEmpty(emergencyContactCommand.ContactName) ? emergencyContactCommand.ContactName : string.Empty,
                        !string.IsNullOrEmpty(emergencyContactCommand.PhoneNumber) ? emergencyContactCommand.PhoneNumber : string.Empty,
                        !string.IsNullOrEmpty(emergencyContactCommand.Relationship) ? emergencyContactCommand.Relationship : string.Empty,
                        !string.IsNullOrEmpty(emergencyContactCommand.Address) ? emergencyContactCommand.Address : string.Empty,
                        emergencyContactCommand.IsPrimaryContact,
                        true,
                        true,
                        editionDate, // CreationDate
                        editionDate // EditionDate
                    );

                    AddEmergencyContactsList.Add(newEmergencyContact);
                }
            }
        }
        if (updatedEmergencyContactsList.Count == 1 && command?.EmergencyContactsList?.Count == 1)
        {
            _emergencyContactRepository.UpdateRangeAsync(updatedEmergencyContactsList);
            _emergencyContactRepository.DeleteRangeEmergencyContacts(emergencyContactsListExisting
                .Where(x => x.CollaboratorId == new CollaboratorId(Guid.Parse(command.CollaboratorId))
                    && x.Id != updatedEmergencyContactsList.FirstOrDefault()?.Id)
                .ToList());
        }
        else
        {
            // Actualizar o agregar los contactos de emergencia modificados o nuevos
            if (updatedEmergencyContactsList.Count > 0 && AddEmergencyContactsList.Count == 0)
            {
                _emergencyContactRepository.UpdateRangeAsync(updatedEmergencyContactsList);
            }
            else if (updatedEmergencyContactsList.Count > 0 && AddEmergencyContactsList.Count > 0)
            {
                _emergencyContactRepository.UpdateRangeAsync(updatedEmergencyContactsList);
                _emergencyContactRepository.AddRangeEmergencyContacts(AddEmergencyContactsList);
            }
            else if (updatedEmergencyContactsList.Count == 0 && AddEmergencyContactsList.Count > 0)
            {
                _emergencyContactRepository.AddRangeEmergencyContacts(AddEmergencyContactsList);
            }
        }

        // Guardar los cambios en la base de datos
        oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
        oldCollaborator.EmailChangedBy = !string.IsNullOrEmpty(command?.EmailChangeBy) ? command.EmailChangeBy : string.Empty;
        oldCollaborator.EditionDate = editionDate;

        _collaboratorRepository.Update(oldCollaborator);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        List<UpdateEmergencyContactResponseFinal> responseContacts = updatedEmergencyContactsList.Union(AddEmergencyContactsList)
         .Select(emergencyContactResponse => new UpdateEmergencyContactResponseFinal(
             emergencyContactResponse.Id.Value,
             emergencyContactResponse.CollaboratorId.Value,
             emergencyContactResponse.ContactName,
             emergencyContactResponse.PhoneNumber,
             emergencyContactResponse.Relationship,
             emergencyContactResponse.Address,
             emergencyContactResponse.IsPrimaryContact))
         .ToList();

        EmergencyContactResponse response = new(
            responseContacts,
            editionDate.Value
            );

        return response;
    }
}