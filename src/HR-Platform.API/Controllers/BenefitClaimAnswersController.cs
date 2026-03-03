using BenefitClaimAnswers.DeleteBenefitFromHistory;
using BenefitClaimAnswers.GetBenefitHistoryNames;
using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BenefitClaimAnswers.Common;
using HR_Platform.Application.BenefitClaimAnswers.Create;
using HR_Platform.Application.BenefitClaimAnswers.GetAllClaimsSent;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using HR_Platform.Infrastructure.MailerServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BenefitClaimAnswers")]
public class BenefitClaimAnswer
(
    ISender mediator,
    IMailerService mailerService,
    IAmazonS3Service amazonS3Service

) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));
    private readonly IMailerService _mailerService = mailerService ?? throw new ArgumentNullException(nameof(mailerService));

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateBaseBenefitClaimAnswersCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        CreateBenefitClaimAnswersCommand result = new
        (
           command.BenefitClaimId,
           command.IsBenefitAccepeted,
           command.Details,
           collaboratorEmail
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("DeleteBenefitFromHistory")]
    public async Task<IActionResult> DeleteBenefitFromHistory([FromBody] DeleteBaseBenefitFromHistoryCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteBenefitFromHistoryCommand result = new
        (
           companyResult.Value.Id,
           command.BenefitName,
           collaboratorEmail
        );

        ErrorOr<List<CollaboratorEmailResponse>> deleteResponse = await _mediator.Send(result);

        if (deleteResponse.IsError)
            return StatusCode(400, new { message = "The benefits of the history could not be eliminated" });

        List<BenefitDeletedMailerDTO> benefitDeletedMailerDTOList = [];

        foreach (CollaboratorEmailResponse email in deleteResponse.Value)
        {
            BenefitDeletedMailerDTO benefitDeletedMailerDTO = new()
            {
                Subject = "Beneficio eliminado",
                To = email.BusinessEmail,
                BenefitName = command.BenefitName,
                Message = command.Message
            };
            benefitDeletedMailerDTOList.Add(benefitDeletedMailerDTO);
        }

        if (deleteResponse.Value.Count > 0)
        {
            await _mailerService.SendBenefitDeletedNotification(benefitDeletedMailerDTOList);
        }

        return Ok();

    }


    [HttpGet("GetClaimsHistory")]
    public async Task<IActionResult> GetAllClaimHistory()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<ClaimSentResponse>> updateResult = await _mediator.Send(new GetAllClaimsSentQuery(companyResult.Value.Id));

        return updateResult.Match(
            Minutes => Ok(Minutes),
            errors => Problem(errors)
        );
    }



    [HttpGet("GetBenefitHistoryNames")]
    public async Task<IActionResult> GetBenefitHistoryName()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
        errors => Problem(errors)
        );

        ErrorOr<List<string>> updateResult = await _mediator.Send(new GetBenefitHistoryNameQuery(companyResult.Value.Id));

        return updateResult.Match(
            Minutes => Ok(Minutes),
            errors => Problem(errors)
        );
    }
}