using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Application.NewCommunications.Common;
using HR_Platform.Application.NewCommunications.Create;
using HR_Platform.Application.NewCommunications.Delete;
using HR_Platform.Application.NewCommunications.GetByCompanyId;
using HR_Platform.Application.NewCommunications.Update;
using HR_Platform.Application.NewCommunications.UpdateIsVisible;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("NewCommunications")]
public class NewCommunication(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetNewCommunicationsByCompanyId")]
    public async Task<IActionResult> GetNewCommunicationsByCompanyId([FromBody] GetBaseNewCommunicationByCompanyIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetNewCommunicationByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id,
            baseQuery.IsVisible
        );

        ErrorOr<List<NewCommunicationFileResponse>> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateNewCommunication")]
    public async Task<IActionResult> Create([FromForm] CreateBaseNewCommunicationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
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

            fileURL = await _amazonS3Service.UploadFile(command.File, "NewCommunicationsFolder");
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

            imageURL = await _amazonS3Service.UploadFile(command.Image, "NewCommunicationsFolder");
            imageName = imageNameAux;
        }

        CreateNewCommunicationsCommand result = new(
            collaboratorEmail,
            companyResult.Value.Id,
            command.Name,
            command.Description,
            fileName,
            fileURL,
            imageName,
            imageURL,
            command.IsSurveyInclude
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateNewCommunication")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseNewCommunicationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
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

            fileURL = await _amazonS3Service.UploadFile(command.File, "NewCommunicationsFolder");
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

            imageURL = await _amazonS3Service.UploadFile(command.Image, "NewCommunicationsFolder");
            imageName = imageNameAux;
        }

        UpdateNewCommunicationsCommand result = new(
            command.NewCommunicationId,
            collaboratorEmail,
            companyResult.Value.Id,
            command.Name,
            command.Description,
            command.IsChangedFile,
            fileName,
            fileURL,
            command.IsChangedImage,
            imageName,
            imageURL,
            command.IsSurveyInclude
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateIsVisibleNewCommunicationById")]
    public async Task<IActionResult> UpdateIsVisibleNewCommunicationById([FromBody] UpdateIsVisibleNewCommunicationCommand baseCommand)
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

    [HttpDelete]
    public async Task<IActionResult> DeleteNewCommunication([FromBody] DeleteNewCommunicationQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(query);

        return result.Match(
            communication => Ok(communication),
            errors => Problem(errors));
    }
}