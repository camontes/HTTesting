using ErrorOr;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Update;

public record UpdateHealthEntitiesCommand
(
    Guid Id,

    string CompanyId,

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

    bool IsEditable,
    bool IsDeleteable
) : IRequest<ErrorOr<bool>>;
