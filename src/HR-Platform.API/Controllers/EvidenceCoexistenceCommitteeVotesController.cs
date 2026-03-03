using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Create;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Common;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.GetByCompanyId;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Delete;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.GetYearsByCompanyId;

namespace HR_Platform.API.Controllers;

[Route("EvidenceCoexistenceCommitteeVote")]
public class EvidenceCoexistenceCommitteeVote(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetEvidenceCoexistenceCommitteeVotesByCompanyId")]
    public async Task<IActionResult> GetEvidenceCoexistenceCommitteeVotesByCompanyId([FromBody] GetBaseEvidenceCoexistenceCommitteeVoteByCompanyIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetEvidenceCoexistenceCommitteeVoteByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id,
            baseQuery.Year
        );

        ErrorOr<EvidenceCoexistenceCommitteeVoteFileAndYearListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetEvidenceCoexistenceCommitteeVoteYearsByCompanyId")]
    public async Task<IActionResult> GetEvidenceCoexistenceCommitteeVoteYearsByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetYearsByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id
        );

        ErrorOr<EvidenceCoexistenceCommitteeVoteYearsListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            minuteYears => Ok(minuteYears),
            errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteEvidenceCoexistenceCommitteeVotesCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            EvidenceCoexistenceCommitteeVotes => Ok(EvidenceCoexistenceCommitteeVotes),
            errors => Problem(errors)
        );
    }

    [HttpPost("CreateEvidenceCoexistenceCommitteeVoteFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseEvidenceCoexistenceCommitteeVoteCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateEvidenceCoexistenceCommitteeVotesObjectCommand> formatFiles = [];

        if (command.EvidenceCoexistenceCommitteeVoteFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.EvidenceCoexistenceCommitteeVoteFiles.Count; i++)
            {
                string fileNameAux = command.EvidenceCoexistenceCommitteeVoteFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.EvidenceCoexistenceCommitteeVoteFiles[i], "EvidenceCoexistenceCommitteeVotesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateEvidenceCoexistenceCommitteeVotesObjectCommand temp = new
                (
                    command.EvidenceCoexistenceCommitteeVoteFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateEvidenceCoexistenceCommitteeVotesCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }
}