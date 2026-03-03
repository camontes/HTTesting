using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Positions.Common;
using HR_Platform.Application.Positions.Create;
using HR_Platform.Application.Positions.Delete;
using HR_Platform.Application.Positions.GetByCompanyId;
using HR_Platform.Application.Positions.Update;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Positions")]
public class Positions(
    IAmazonS3Service amazonS3Service,

    ISender mediator
    ) : ApiController
{
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetPositionsByCompany")]
    public async Task<IActionResult> GetPositionsByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetPositionsByCompanyIdQuery query = new(companyResult.Value.Id);

        ErrorOr<List<PositionsResponse>> positionsResult = await _mediator.Send(query);

        return positionsResult.Match(
            positions => Ok(positions),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] BaseCreatePositionsCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string logoURL = string.Empty;
        string fileName = string.Empty;

        if (command.PositionFile != null)
        {
            string fileNameAux = command.PositionFile.FileName
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

            logoURL = await _amazonS3Service.UploadFile(command.PositionFile, "PositionFilesFolder");
            fileName = fileNameAux;

            if (string.IsNullOrEmpty(logoURL))
                return StatusCode(400, new { message = "No se pudo crear el cargo" });
        }

        CreatePositionsCommand finalPositionCommand = new
        (
            companyResult.Value.Id.ToString(),

            command.Name,
            command.NameEnglish,

            command.Description,
            command.DescriptionEnglish,

            !string.IsNullOrEmpty(logoURL) ? logoURL : string.Empty,
            !string.IsNullOrEmpty(fileName)? fileName : string.Empty,

            true, // IsEditable
            true // IsDeleteable
        );

        ErrorOr<Guid> createResult = await _mediator.Send(finalPositionCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromForm] UpdateBasePositionCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string positionFile = string.Empty;
        string fileName = string.Empty;


        if (command.PositionFile != null)
        {
            string fileNameAux = command.PositionFile.FileName
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

            positionFile = await _amazonS3Service.UploadFile(command.PositionFile, "PositionFilesFolder");
            fileName = fileNameAux;

            if (string.IsNullOrEmpty(positionFile))
                return StatusCode(400, new { message = "No se pudo crear el cargo" });
        }

        UpdatePositionCommand finalPositionCommand = new
        (
            command.Id,
            command.Name,
            command.Description,
            !string.IsNullOrEmpty(positionFile) ? positionFile : string.Empty,
            !string.IsNullOrEmpty(fileName) ? fileName : string.Empty
        );

        ErrorOr<bool> createResult = await _mediator.Send(finalPositionCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
      );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeletePositionCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> updateResult = await _mediator.Send(baseCommand);

        return updateResult.Match(
            Position => Ok(Position),
            errors => Problem(errors)
        );
    }
}