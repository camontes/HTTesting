using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.Forms.ChangeNoveltyStateById;
using HR_Platform.Application.Forms.Common;
using HR_Platform.Application.Forms.Create;
using HR_Platform.Application.Forms.CreateAnswerByFormId;
using HR_Platform.Application.Forms.GetByAreaId;
using HR_Platform.Application.Forms.GetNoveltyByCollaboratorId;
using HR_Platform.Application.Forms.GetNoveltyByCompanyId;
using HR_Platform.Application.Forms.UpdateIsVisibleForm;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Forms")]
public class Form
(
    IAmazonS3Service amazonS3Service,

    ISender mediator
)
:
ApiController
{
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("CreateForm")]
    public async Task<IActionResult> Create([FromBody] CreateBaseFormCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateFormCommand request = new(
            companyResult.Value.Id,
            command.Name,
            command.Description,
            command.NoveltyTypeId,
            command.QuestionTypeRequests
        );

        ErrorOr<bool> result = await _mediator.Send(request);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetFormByAreaId")]
    public async Task<IActionResult> GetFormByAreaId([FromBody] GetFormByAreaIdQuery query)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<FormsResponse>> result = await _mediator.Send(query);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("CreateAnswerByFormId")]
    public async Task<IActionResult> CreateAnswerByFormId([FromBody] CreateBaseAnswerByFormIdCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateAnswerByFormIdCommand request = new
        (
            collaboratorEmail,
            command.AnswerObjects,
            command.NoveltyTypeId
        );

        ErrorOr<bool> result = await _mediator.Send(request);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetNoveltyByCollaboratorId")]
    public async Task<IActionResult> GetNoveltyByCollaboratorId([FromBody] GetBaseNoveltyByCollaboratorIdQuery query)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetNoveltyByCollaboratorIdQuery request = new
        (
            collaboratorEmail,

            query.WithResponses,

            query.NoveltyTypeId,

            query.CollaboratorName,

            query.FormName,

            query.Page,
            query.PageSize
        );

        ErrorOr<List<NoveltyByCollaboratorResponse>> result = await _mediator.Send(request);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetNoveltyByCompanyId")]
    public async Task<IActionResult> GetNoveltyByCompanyId([FromBody] GetBaseNoveltyByCompanyIdQuery query)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetNoveltyByCompanyIdQuery request = new
        (
            companyResult.Value.Id,

            collaboratorEmail,

            query.NoveltyTypeId,

            query.CollaboratorName,

            query.Page,
            query.PageSize
        );

        ErrorOr<List<NoveltyByCollaboratorResponse>> result = await _mediator.Send(request);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetQuestionsByFormId")]
    public async Task<IActionResult> GetQuestionsByFormId([FromBody] GetQuestionsByFormIdQuery query)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<FormQuestionsResponse> result = await _mediator.Send(query);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateIsVisibleFormById")]
    public async Task<IActionResult> UpdateIsVisibleFormById([FromBody] UpdateIsVisibleFormCommand baseCommand)
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

    [HttpPatch("ChangeNoveltyStateById")]
    public async Task<IActionResult> ChangeNoveltyStateById([FromBody] ChangeBaseNoveltyStateByIdCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateFormAnswerStateFiles> formatFiles = [];

        if (baseCommand.Files is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < baseCommand.Files.Count; i++)
            {
                string fileNameAux = baseCommand.Files[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(baseCommand.Files[i], "FormAnswerStatesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateFormAnswerStateFiles temp = new
                (
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );

                formatFiles.Add(temp);
            }
        }

        ChangeNoveltyStateByIdCommand command = new
        (
            baseCommand.FormAnswerGroupId,

            baseCommand.SurveyId,

            baseCommand.FormAnswerGroupStateId,

            baseCommand.DescriptionState,

            formatFiles
        );

        ErrorOr<bool> result = await _mediator.Send(command);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}