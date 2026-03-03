using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateLocation;
public record UpdateLocationCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    string CompanyId,
    DateTime? Birthdate,
    string? Country,
    string? Department,
    string? City,
    string? EconomicLevelId,
    string? LocationAddress,
    string? PhoneNumber,
    string? PostalCode
) : IRequest<ErrorOr<UpdateLocationResponse>>;
