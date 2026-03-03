using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateEducationData;
public record UpdateEmergencyContactCommand(
    string Id,
    string CollaboratorId,
    string? ContactName,
    string? PhoneNumber,
    string? Relationship,
    string? Address,
    bool IsPrimaryContact

) : IRequest<ErrorOr<EmergencyContactResponse>>;
