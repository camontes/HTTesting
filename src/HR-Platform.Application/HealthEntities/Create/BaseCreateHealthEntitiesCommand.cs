using ErrorOr;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Create;

public record BaseCreateHealthEntitiesCommand(List<BaseHealthEntityCommand> HealthEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseHealthEntityCommand(
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
);

