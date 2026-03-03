namespace HR_Platform.Application.Common;

public record AddressResponse(
    string StreetAddress,
    int CountryCode,
    string Country,
    int StateCode,
    string State,
    int CityCode,
    string City,
    string ZipCode
);
