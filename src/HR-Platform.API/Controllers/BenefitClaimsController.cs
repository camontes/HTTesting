using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BenefitClaimEntities.Create;
using HR_Platform.Application.BenefitClaims.Common;
using HR_Platform.Application.BenefitClaims.Create;
using HR_Platform.Application.BenefitClaims.GetAllClaimsByCompanyId;
using HR_Platform.Application.BenefitClaims.ValidationClaim;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BenefitClaim")]
public class BenefitClaims(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [HttpGet]
    public async Task<IActionResult> GetAllClaims()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<BenefitClaimsResponse>> result = await _mediator.Send(new GetAllClaimsByCompanyIdQuery(companyResult.Value.Id));

        return result.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseBenefitClaimsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );


        CreateBenefitClaimsCommand result = new
        (
            companyResult.Value.Id,
            command.BenefitId,
            companyEmail
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPost("GetByBenefitClaim")]
    public async Task<IActionResult> GetByBenefitClaim([FromBody] GetAllClaimsByCollaboratorIdQuery query)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<CollaboratorBenefitClaimsResponse> createResult = await _mediator.Send(query);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPost("ValidationClaim")]
    public async Task<IActionResult> ValidationClaim([FromBody] BaseValidationClaimQuery query)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ValidationClaimQuery result = new
        (
            query.BenefitId,
            companyEmail
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }
}