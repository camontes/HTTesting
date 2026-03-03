using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Companies.Update;

public record BaseUpdateCompanyCommand(
    string Id,

    string Email,
    string? RequestsEmail,

    string CompanyName,
    string? MenuName,

    string? StreetAddress,
    int CountryCode,
    string? Country,
    int StateCode,
    string? State,
    int CityCode,
    string? City,
    string? ZipCode,

    string? PhoneNumber,

    IFormFile? Logo,
    string? LogoURL,
    string? LogoName,

    string? URL,

    string CreationDate
) : IRequest<ErrorOr<Guid>>;
