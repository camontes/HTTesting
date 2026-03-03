using ErrorOr;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Create;

public record CreateHealthEntitiesCommand(List<HealthEntityCommand> HealthEntitiesList) : IRequest<ErrorOr<bool>>;

public record HealthEntityCommand(
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
);

