using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.ActiveBreaks.Common;
using HR_Platform.Application.ActiveBreaks.Create;
using HR_Platform.Application.ActiveBreaks.GetByCompanyId;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ActiveBreaks.UpdateIsVisible;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.ActiveBreaks.Update;
using HR_Platform.Application.Assignations.Delete;
using HR_Platform.Application.ActiveBreaks.Delete;

namespace HR_Platform.API.Controllers;

[Route("ActiveBreaks")]
public class ActiveBreaks
(
    ISender mediator,

    IAmazonS3Service amazonS3Service
)
:
ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));


    [HttpPost("CreateActiveBreak")]
    public async Task<IActionResult> Create([FromForm] CreateBaseActiveBreakCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match
        (
            company => Ok(company),
            errors => Problem(errors)
        );

        string fileURL = string.Empty;
        string fileName = string.Empty;
        string imageURL = string.Empty;
        string imageName = string.Empty;

        if (command.File != null)
        {
            string fileNameAux = command.File.FileName
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

            fileURL = await _amazonS3Service.UploadFile(command.File, "ActiveBreaksFolder");

            fileName = fileNameAux;
        }

        if (command.Image != null)
        {
            string imageNameAux = command.Image.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.Image, "ActiveBreaksFolder");

            imageName = imageNameAux;
        }

        CreateActiveBreakCommand result = new
        (
            command.Name,
            command.Description,

            imageName,
            imageURL,

            fileName,
            fileURL,

            collaboratorEmail,

            companyResult.Value.Id
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match
        (
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetActiveBreaksByCompanyId")]
    public async Task<IActionResult> GetActiveBreaksByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetActiveBreaksByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id
        );

        ErrorOr<List<ActiveBreakResponse>> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("UpdateActiveBreakIsVisibleFormById")]
    public async Task<IActionResult> UpdateActiveBreakIsVisibleFormById([FromBody] UpdateActiveBreakIsVisibleCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseCommand);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("UpdateActiveBreak")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseActiveBreakCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match
        (
            company => Ok(company),
            errors => Problem(errors)
        );

        string fileURL = string.Empty;
        string fileName = string.Empty;
        string imageURL = string.Empty;
        string imageName = string.Empty;

        if (command.File != null)
        {
            string fileNameAux = command.File.FileName
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

            fileURL = await _amazonS3Service.UploadFile(command.File, "ActiveBreaksFolder");

            fileName = fileNameAux;
        }
        else
        {
            fileName = command.FileName;
            fileURL = command.FileURL;
        }

        if (command.Image != null)
        {
            string imageNameAux = command.Image.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.Image, "ActiveBreaksFolder");

            imageName = imageNameAux;
        }
        else
        {
            imageName = command.ImageName;
            imageURL = command.ImageURL;
        }

        UpdateActiveBreakCommand result = new
        (
            command.Id,

            command.Name,
            command.Description,

            imageName,
            imageURL,

            fileName,
            fileURL,

            collaboratorEmail,

            companyResult.Value.Id
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match
        (
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteActiveBreakCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> deleteResult = await _mediator.Send(baseCommand);

        return deleteResult.Match(
            assignation => Ok(assignation),
            errors => Problem(errors)
        );

    }
}