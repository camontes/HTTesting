using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.Create;
using HR_Platform.Application.Companies.GetAll;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Companies.Update;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Companies")]
public class Companies(
    IAmazonS3Service amazonS3Service,

    ISender mediator
    ) : ApiController
{
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateCompanyCommand command)
    {
        if (command.Logo != null)
        {
            string logoURL = await _amazonS3Service.UploadFile(command.Logo, "CompanyLogosFolder");

            if(string.IsNullOrEmpty(logoURL))
                return StatusCode(400, new { message = "No se pudo crear la empresa" });
        }

        ErrorOr<Guid> createResult = await _mediator.Send(command);

        return createResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        ErrorOr<IReadOnlyList<CompaniesResponse>> companiesResult = await _mediator.Send(new GetAllCompaniesQuery());

        return companiesResult.Match(
            companies => Ok(companies),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetByCompanyId")]
    public async Task<IActionResult> GetById()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        return companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
    }

    [HttpPatch]
    public async Task<IActionResult> Edit([FromForm] BaseUpdateCompanyCommand command)
    {
        string logoURL = command.LogoURL;
        string logoName = command.LogoName;

        if (command.Logo != null)
        {
            string photoNameAux = command.Logo.FileName
                .Replace("\\", "-")
                .Replace("/", "-")
                .Replace(":", "-")
                .Replace("*", "-")
                .Replace("?", "-")
                .Replace("\"", "-")
                .Replace("<", "-")
                .Replace(">", "-")
                .Replace("|", "-")
                .Replace("#", "-")
                .Replace("$", "-");

            logoURL = await _amazonS3Service.UploadFile(command.Logo, "CompanyLogosFolder");
            logoName = photoNameAux;

            if (string.IsNullOrEmpty(logoURL))
                return StatusCode(400, new { message = "No se pudo editar la información" });
        }
        else if(logoURL == string.Empty || logoName == string.Empty)
        {
            logoURL = string.Empty;
            logoName = string.Empty;
        }

        UpdateCompanyCommand uploadCommand = new
            (
                command.Id,

                command.Email,
                !string.IsNullOrEmpty(command.RequestsEmail) ? command.RequestsEmail : string.Empty,

                command.CompanyName,
                !string.IsNullOrEmpty(command.MenuName) ? command.MenuName : string.Empty,

                command.StreetAddress,
                command.CountryCode,
                !string.IsNullOrEmpty(command.Country) ? command.Country : string.Empty,
                command.StateCode,
                !string.IsNullOrEmpty(command.State) ? command.State : string.Empty,
                command.CityCode,
                !string.IsNullOrEmpty(command.City) ? command.City : string.Empty,
                !string.IsNullOrEmpty(command.ZipCode) ? command.ZipCode : string.Empty,

                command.PhoneNumber,

                command.Logo,
                !string.IsNullOrEmpty(logoURL) ? logoURL : string.Empty,
                !string.IsNullOrEmpty(logoName) ? logoName : string.Empty,

                !string.IsNullOrEmpty(command.URL) ? command.URL : string.Empty,

                command.CreationDate
            );

        ErrorOr<Guid> createResult = await _mediator.Send(uploadCommand);

        return createResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }
}