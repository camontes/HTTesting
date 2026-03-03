using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Notes.Common;
using HR_Platform.Application.Notes.Create;
using HR_Platform.Application.Notes.CreateAnswer;
using HR_Platform.Application.Notes.GetByCollaboratorId;
using HR_Platform.Application.Notes.GetNoteForCollaborator;
using HR_Platform.Application.Notes.GetNotificationByCollaborator;
using HR_Platform.Application.Notes.Update;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Notes")]
public class Note(
    ISender mediator,
    IAmazonS3Service amazonS3Service
    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpGet("GetNotificationByCollaborator")]
    public async Task<IActionResult> GetNotificationByCollaborator()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company), 
            errors => Problem(errors)
        );

        ErrorOr<NotificationNotesResponse> result = await _mediator.Send(new GetNotificationByCollaboratorQuery(companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpPost("GetNotesByCollaboratorId")]
    public async Task<IActionResult> GetNotesByCompanyId([FromBody] GetBaseNoteByCollaboratorIdQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetNoteByCollaboratorIdQuery request = new
        (
            companyEmail,
            query.CollaboratorId,
            query.IsPublic
        );

        ErrorOr<List<NotesResponse>> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetNotesForCollaborator")]
    public async Task<IActionResult> GetNotesForCollaborator([FromBody] GetBaseNoteForCollaboratorQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetNoteForCollaboratorQuery request = new
        (
            companyEmail,
            query.IsPublic
        );

        ErrorOr<List<NotesResponse>> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateNote")]
    public async Task<IActionResult> Create([FromForm] CreateBaseNoteCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateNotesObjectFile> formatFiles = [];

        if (command.NoteFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.NoteFiles.Count; i++)
            {
                string fileNameAux = command.NoteFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.NoteFiles[i], "NoteFilesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateNotesObjectFile temp = new
                (
                    command.NoteFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateNoteCommand result = new
        (
            collaboratorEmail,
            command.Description,
            command.CollaboratorId,
            command.IsPublic,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("CreateNoteAnswer")]
    public async Task<IActionResult> CreateNoteAnswer([FromForm] CreateBaseNoteAnswerCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<NoteAnswersFileObject> formatFiles = [];

        if (command.NoteAnswerFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.NoteAnswerFiles.Count; i++)
            {
                string fileNameAux = command.NoteAnswerFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.NoteAnswerFiles[i], "NoteFilesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                NoteAnswersFileObject temp = new
                (
                    command.NoteAnswerFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateNoteAnswerCommand result = new
        (
            collaboratorEmail,
            command.Description,
            command.MainNoteId,
            null,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }


    [HttpPatch("HideNotificationNote")]
    public async Task<IActionResult> HideNotificationNote()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new HideNotificationNoteQuery(companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("EditNote")]
    public async Task<IActionResult> Edit([FromForm] UpdateBaseNoteCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<UpdateNotesObjectCommand> formatFiles = [];

        if (command.NoteFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.NoteFiles.Count; i++)
            {
                string fileNameAux = command.NoteFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.NoteFiles[i], "NoteFilesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                UpdateNotesObjectCommand temp = new
                (
                    command.NoteFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );

                formatFiles.Add(temp);
            }
        }

        UpdateNoteCommand result = new
        (
            command.Id,

            command.Description,

            command.IsPublic,

            collaboratorEmail,

            formatFiles,

            command.NoteFilesIdsDelete,

            command.NoteFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }
}