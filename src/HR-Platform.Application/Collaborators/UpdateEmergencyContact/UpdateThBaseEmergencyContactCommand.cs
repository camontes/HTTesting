using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateEducationData;
public record UpdateThBaseEmergencyContactCommand(
    string CollaboratorId,
    List<UpdateEmergencyContactCommand>? EmergencyContactsList

) : IRequest<ErrorOr<EmergencyContactResponse>>;
