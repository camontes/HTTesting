using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BirthdayTemplateSettings.Common;
using HR_Platform.Application.BirthdayTemplateSettings.GetByCompanyId;
using HR_Platform.Application.BirthdayTemplateSettings.Update;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BirthdayTemplateSettings")]
public class BirthdayTemplateSettings(ISender mediator, IAmazonS3Service amazonS3Service) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpGet("GetByCompanyId")]
    public async Task<IActionResult> GetByCompanyId()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<BirthdayTemplateSettingsResponse> result = await _mediator.Send(new GetBirthdayTemplateSettingsByCompanyIdQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));

    }

    [HttpPatch("UpdateTemplateOptions")]
    public async Task<IActionResult> UpdateTemplateOption([FromForm] UpdateBaseBirthdayTemplateSettingsCommand command)
    {
        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string customTemplateURL = string.Empty;
        string customTemplateName = string.Empty;

        if (command.Template is not null)
        {
            customTemplateURL = await _amazonS3Service.UploadFile(command.Template, "MinutesFolder");
            customTemplateName = command.Template.FileName;

            if (string.IsNullOrEmpty(customTemplateURL))
                return StatusCode(400, new { message = $"It´s not possible create a URL for {customTemplateName}" });
        }

        UpdateBirthdayTemplateSettingsCommand request = new
        (
            companyResult.Value.Id,
            command.IsDefaultTemplate,
            command.IsAllowSendPost,
            command.TemplateMessage,
            customTemplateURL,
            customTemplateName
        );

        ErrorOr<bool> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetCollaboratorsWhoAreCelebratingTheirBirthdays")]
    public async Task<IActionResult> GetCollaboratorsWhoAreCelebratingTheirBirthdays()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<BirthdayCollaboratorsResponse> result = await _mediator.Send(new GetCollaboratorsQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));

    }

}