using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateEducationData;
public record UpdateBaseEmergencyContactCommand(
    string EmailChangeBy,

    string CollaboratorId,
    List<UpdateEmergencyContactCommand>? EmergencyContactsList

) : IRequest<ErrorOr<EmergencyContactResponse>>;
