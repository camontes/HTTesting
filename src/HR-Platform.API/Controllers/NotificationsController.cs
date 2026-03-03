using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Notifications.Common;
using HR_Platform.Application.Notifications.GetAllClaimsByCompanyId;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HR_Platform.Application.Notifications.MarkAsRead;
using HR_Platform.Application.Notifications.MarkAllAsReadByCollaborator;
using HR_Platform.Application.Notifications.Delete;
using HR_Platform.Application.Notifications.DeleteAllReadByCollaborator;

namespace HR_Platform.API.Controllers;

[Route("Notifications")]
public class Notifications(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [HttpGet]
    public async Task<IActionResult> GetAllNotificationsByCollaborator()
    {

        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<NotificationResponse> result = await _mediator.Send(new GetAllByCollaboratorIdQuery(collaboratorEmail));

        return result.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch("MarkAsRead")]
    public async Task<IActionResult> MarkAsRead([FromBody] MarkNotificationAsReadCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> deleteResponse = await _mediator.Send(command);

        if (deleteResponse.Value)
            return StatusCode(200, new { message = "Notification readed successfully", deleteResponse.Value });

        return StatusCode(400, new { message = "Notification not readed", deleteResponse.Value });
    }

    [HttpPatch("MarkAllAsReadByCollaborator")]
    public async Task<IActionResult> MarkAllAsReadByCollaborator()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        MarkAllAsReadByCollaboratorCommand command = new(collaboratorEmail);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> deleteResponse = await _mediator.Send(command);

        if (deleteResponse.Value)
            return StatusCode(200, new { message = "Notifications readed successfully", deleteResponse.Value });

        return StatusCode(400, new { message = "Notifications not readed", deleteResponse.Value });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteNotificationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> deleteResponse = await _mediator.Send(command);

        if (deleteResponse.Value)
            return StatusCode(200, new { message = "Notification deleted successfully", deleteResponse.Value });

        return StatusCode(400, new { message = "Notification not deleted", deleteResponse.Value });
    }

    [HttpDelete("DeleteAllReadByCollaborator")]
    public async Task<IActionResult> DeleteAllReadByCollaborator()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        DeleteAllReadByCollaboratorCommand command = new(collaboratorEmail);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> deleteResponse = await _mediator.Send(command);

        if (deleteResponse.Value)
            return StatusCode(200, new { message = "Notifications deleted successfully", deleteResponse.Value });

        return StatusCode(400, new { message = "Notifications not deleted", deleteResponse.Value });
    }
}