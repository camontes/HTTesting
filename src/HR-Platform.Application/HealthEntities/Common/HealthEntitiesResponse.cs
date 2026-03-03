namespace HR_Platform.Application.HealthEntities.Common;

public record HealthEntitiesResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    string? StreetAddress,
    int CountryCode,
    string? Country,
    int StateCode,
    string? State,
    int CityCode,
    string? City,
    string? ZipCode,

    int CollaboratorsCount,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
