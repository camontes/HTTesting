using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Application.EmergencyPlans.Common;
using HR_Platform.Application.EmergencyPlans.Create;
using HR_Platform.Application.EmergencyPlans.GetAllByEmergencyPlanType;
using HR_Platform.Application.EmergencyPlans.GetById;
using HR_Platform.Application.EmergencyPlans.Update;
using HR_Platform.Application.EmergencyPlans.UpdateIsVisible;
using HR_Platform.Application.EmergencyPlans.Delete;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("EmergencyPlan")]
public class EmergencyPlan(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetAllEmergencyPlansByCompanyId")]
    public async Task<IActionResult> GetEmergencyPlansByCompanyId([FromBody] GetBaseAllEmergencyPlansQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetAllEmergencyPlansQuery response = new
        (
            companyResult.Value.Id,
            baseQuery.IsVisible
        );

        ErrorOr<EmergencyPlanResponse> result = await _mediator.Send(response);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetEmergencyPlanTypesById")]
    public async Task<IActionResult> GetEmergencyPlansByCompanyId([FromBody] GetEmergencyPlanByIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<EmergencyPlanAllContentResponse>> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateEmergencyPlan")]
    public async Task<IActionResult> Create([FromForm] CreateBaseEmergencyPlanCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string imageURL = string.Empty;
        string imageName = string.Empty;
        string videoURL = string.Empty;
        string videoName = string.Empty;

        if (command.ImageFile != null)
        {
            string imageNameAux = command.ImageFile.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.ImageFile, "EmergencyPlanFolder");
            imageName = imageNameAux;
        }

        if (command.VideoFile != null)
        {
            string videoNameAux = command.VideoFile.FileName
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

            videoURL = await _amazonS3Service.UploadFile(command.VideoFile, "EmergencyPlanFolder");
            videoName = videoNameAux;
        }

        CreateEmergencyPlansCommand result = new(
            companyResult.Value.Id,
            command.EmergencyPlanTypeId,
            command.Description,
            imageName,
            imageURL,
            videoURL,
            videoName
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateEmergencyPlan")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseEmergencyPlanCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string imageURL = string.Empty;
        string imageName = string.Empty;
        string videoURL = string.Empty;
        string videoName = string.Empty;

        if (command.ImageFile != null)
        {
            string imageNameAux = command.ImageFile.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.ImageFile, "EmergencyPlanFolder");
            imageName = imageNameAux;
        }

        if (command.VideoFile != null)
        {
            string videoNameAux = command.ImageFile.FileName
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

            videoURL = await _amazonS3Service.UploadFile(command.VideoFile, "EmergencyPlanFolder");
            videoName = videoNameAux;
        }

        UpdateEmergencyPlansCommand result = new(
            companyResult.Value.Id,
            command.EmergencyPlanId,
            command.EmergencyPlanTypeId,
            command.Description,
            command.IsUpdateImageFile,
            command.IsUpdateVideoFile,
            imageName,
            imageURL,
            videoURL,
            videoName
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateIsVisibleEmergencyPlanById")]
    public async Task<IActionResult> UpdateIsVisibleEmergencyPlanById([FromBody] UpdateIsVisibleEmergencyPlanCommand baseCommand)
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
    public async Task<IActionResult> Delete([FromBody] BaseDeleteEmergencyPlansCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteEmergencyPlansCommand command = new
        (
            baseCommand.EmergencyPlanId,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            EmergencyPlans => Ok(EmergencyPlans),
            errors => Problem(errors)
        );
    }
}