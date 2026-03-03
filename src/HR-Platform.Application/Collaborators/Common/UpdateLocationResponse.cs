using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Application.Collaborators.Common;

public record UpdateLocationResponse
(
    Guid CollaboratorId,
    string CompanyId,
    DateTime? Birthdate,
    string? Country,
    string? Department,
    string? City,
    string? EconomicLevelId,
    string? LocationAddress,
    string? PhoneNumber,
    string? PostalCode,
    TimeDate editionDate
);
