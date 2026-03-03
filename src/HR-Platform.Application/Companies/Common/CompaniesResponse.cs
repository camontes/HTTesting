namespace HR_Platform.Application.Companies.Common;

public record CompaniesResponse(
    Guid Id,

    string CompanyName,
    string SuperAdminName,
    string SuperAdminPhoto,
    string SuperAdminPhotoName,
    string SuperAdminPhoneNumber,
    string? MenuName,

    string? StreetAddress,
    int CountryCode,
    string? Country,
    int StateCode,
    string? State,
    int CityCode,
    string? City,
    string? ZipCode,

    string SuperAdminEmail,
    string CompanyEmail,
    string? RequestsEmail,

    string CompanyPhoneNumber, 

    string? LogoURL,
    string LogoName,

    string? URL,

    string CreationDate
);
