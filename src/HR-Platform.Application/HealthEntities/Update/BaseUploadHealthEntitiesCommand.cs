using ErrorOr;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Update;

public record BaseUpdateHealthEntitiesCommand
(
    Guid Id,

    string Name,
    string NameEnglish,

    string? StreetAddress,
    int CountryCode,
    string? Country,
    int StateCode,
    string? State,
    int CityCode,
    string? City,
    string? ZipCode

) : IRequest<ErrorOr<bool>>;
