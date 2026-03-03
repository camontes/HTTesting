using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.ImprovementPlans.Common;
using HR_Platform.Application.ImprovementPlans.Create;
using HR_Platform.Application.ImprovementPlans.CreateResponses;
using HR_Platform.Application.ImprovementPlans.GetByCollaboratorCriteriaAnswerId;
using HR_Platform.Application.ImprovementPlans.GetByCollaboratorId;
using HR_Platform.Application.ImprovementPlans.GetByEvaluatorId;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("ImprovementPlan")]
public class ImprovementPlan(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));


    [HttpPost("GetByCollaboratorCriteriaAnswerId")]
    public async Task<IActionResult> GetByCollaboratorCriteriaAnswerId([FromBody] GetByCollaboratorCriteriaAnswerIdQuery query)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<ImprovementPlanResponse>> updateResult = await _mediator.Send(query);

        return updateResult.Match(
            Minutes => Ok(Minutes),
            errors => Problem(errors)
        );
    }

    [HttpPost("CreateImprovementPlanFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseImprovementPlanCommand command)
    {
        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateImprovementPlanObject> task = [];

        if (!string.IsNullOrEmpty(command.TaskRequestsJson))
        {
            var taskRequests = JsonConvert.DeserializeObject<List<TaskRequest>>(command.TaskRequestsJson);

            int varAux = 1;

            string fileURL = string.Empty;
            string fileName = string.Empty;
            Dictionary<string, string> files = [];

            if (command.ImprovementPlanFiles is not null && command.ImprovementPlanFiles.Count > 0)
            {
                try
                {
                    for (int i = 0; i < command.ImprovementPlanFiles.Count; i++)
                    {
                        string fileNameAux = command.ImprovementPlanFiles[i].FileName
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

                        fileURL = await _amazonS3Service.UploadFile(command.ImprovementPlanFiles[i], "ImprovementPlansFolder");
                        fileName = fileNameAux;

                        if (string.IsNullOrEmpty(fileURL))
                            return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                        files.Add(fileName, fileURL);
                    }
                }
                catch(Exception ex)
                {
                    files.Add(fileName + "(" + varAux + ")", fileURL);

                    varAux++;
                }
            }

            if (taskRequests is not null && taskRequests.Count > 0)
            {
                foreach (var taskRequest in taskRequests)
                {
                    List<CreateImprovementPlanFiles> formatFiles = [];

                    for (int i = 0; i < taskRequest.FileNames.Count; i++)
                    {
                        string fileNameReplaced = taskRequest.FileNames[i]
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

                        CreateImprovementPlanFiles temp = new
                        (
                            fileNameReplaced,
                            files.TryGetValue(fileNameReplaced, out string filePath) ? filePath : string.Empty
                        );
                        formatFiles.Add(temp);
                    }

                    CreateImprovementPlanObject taskResult = new
                    (
                        taskRequest.TaskDescription,
                        formatFiles
                    );
                    task.Add(taskResult);
                }
            }
        }


        CreateImprovementPlanCommand result = new
        (
            collaboratorEmail,
            command.CollaboratorCriteriaAnswerId,
            task
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetByEvaluatorId")]
    public async Task<IActionResult> GetByEvaluatorId([FromBody] GetBaseByEvaluatorIdQuery baseQuery)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetByEvaluatorIdQuery query = new(collaboratorEmail, baseQuery.CollaboratorName, baseQuery.WithResponses, baseQuery.Page, baseQuery.PageSize);

        ErrorOr<ImprovementPlansByEvaluatorResponse> queryResult = await _mediator.Send(query);

        return queryResult.Match(
            improvementPlans => Ok(improvementPlans),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetByCollaboratorId")]
    public async Task<IActionResult> GetByCollaboratorId([FromBody] GetBaseByCollaboratorIdQuery baseQuery)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetByCollaboratorIdQuery query = new(collaboratorEmail, baseQuery.CollaboratorName, baseQuery.WithResponses, baseQuery.Page, baseQuery.PageSize);

        ErrorOr<ImprovementPlansByCollaboratorResponse> queryResult = await _mediator.Send(query);

        return queryResult.Match(
            improvementPlans => Ok(improvementPlans),
            errors => Problem(errors)
        );
    }

    [HttpPost("CreateImprovementPlanResponses")]
    public async Task<IActionResult> CreateImprovementPlanResponses([FromForm] CreateBaseImprovementPlanResponsesCommand command)
    {
        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateImprovementPlanResponseObject> response = [];

        if (!string.IsNullOrEmpty(command.TaskResponsesJSON))
        {
            List<ImprovementPlanResponsesRequest> responseRequests = JsonConvert.DeserializeObject<List<ImprovementPlanResponsesRequest>>(command.TaskResponsesJSON);

            int varAux = 1;

            string fileURL = string.Empty;
            string fileName = string.Empty;

            Dictionary<string, string> files = [];

            if (command.ImprovementPlanResponseFiles is not null && command.ImprovementPlanResponseFiles.Count > 0)
            {
                try
                {
                    for (int i = 0; i < command.ImprovementPlanResponseFiles.Count; i++)
                    {
                        string fileNameAux = command.ImprovementPlanResponseFiles[i].FileName
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

                        fileURL = await _amazonS3Service.UploadFile(command.ImprovementPlanResponseFiles[i], "ImprovementPlansFolder");

                        fileName = fileNameAux;

                        if (string.IsNullOrEmpty(fileURL))
                            return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                        files.Add(fileName, fileURL);
                    }
                }
                catch (Exception ex)
                {
                    files.Add(fileName + "(" + varAux + ")", fileURL);

                    varAux++;
                }
            }

            if (responseRequests is not null && responseRequests.Count > 0)
            {
                foreach (ImprovementPlanResponsesRequest responseRequest in responseRequests)
                {
                    List<CreateImprovementPlanResponseFiles> formatFiles = [];

                    for (int i = 0; i < responseRequest.FileNames.Count; i++)
                    {
                        string fileNameReplaced = responseRequest.FileNames[i]
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

                        CreateImprovementPlanResponseFiles temp = new
                        (
                            fileNameReplaced,

                            files.TryGetValue(fileNameReplaced, out string filePath) ? filePath : string.Empty
                        );

                        formatFiles.Add(temp);
                    }

                    CreateImprovementPlanResponseObject responseResult = new
                    (
                        responseRequest.ImprovementPlanTaskId,

                        responseRequest.ResponseDescription,

                        formatFiles
                    );

                    response.Add(responseResult);
                }
            }
        }


        CreateImprovementPlanResponsesCommand result = new
        (
            command.ImprovementPlanId,

            collaboratorEmail,

            response
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }
}