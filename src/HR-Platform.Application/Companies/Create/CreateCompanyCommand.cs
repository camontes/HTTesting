using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Companies.Create;

public record CreateCompanyCommand(
    string Email,
    string RequestsEmail,

    string CompanyName, 
    string? MenuName,

    string StreetAddress,
    int CountryCode,
    string Country,
    int StateCode,
    string State,
    int CityCode,
    string City,
    string ZipCode,

    string PhoneNumber,
    string ProfessionalCard,

    IFormFile Logo,
    string LogoURL,
    string LogoName,

    string? URL,

    string Name,

    string Document
) : IRequest<ErrorOr<Guid>>;
