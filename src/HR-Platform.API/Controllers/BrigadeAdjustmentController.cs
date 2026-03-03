using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BrigadeAdjustmentEntities.Create;
using HR_Platform.Application.BrigadeAdjustments.Common;
using HR_Platform.Application.BrigadeAdjustments.Create;
using HR_Platform.Application.BrigadeAdjustments.Delete;
using HR_Platform.Application.BrigadeAdjustments.GetByCompanyId;
using HR_Platform.Application.BrigadeAdjustments.Update;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BrigadeAdjustment")]
public class BrigadeAdjustments(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetBrigadeAdjustmentsByCompanyId")]
    public async Task<IActionResult> GetBrigadeAdjustmentsByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<BrigadeAdjustmentsAndCountResponse> result = await _mediator.Send(new GetBrigadeAdjustmentsByCompanyIdQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseBrigadeAdjustmentsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<BrigadeAdjustmentData> brigadeAdjustmentDataResult = [];

        if (command is not null && command.BrigadeAdjustmentList is not null && command.BrigadeAdjustmentList.Count > 0)
        {
            foreach (BaseBrigadeAdjustmentEntityCommand brigadeAdjustmentCommand in command.BrigadeAdjustmentList)
            {
                BrigadeAdjustmentData brigadeAdjustmentdData = new
                (
                   brigadeAdjustmentCommand.Name,
                   brigadeAdjustmentCommand.Name,
                   brigadeAdjustmentCommand.IconId
                 );
                brigadeAdjustmentDataResult.Add(brigadeAdjustmentdData);
            }
        }

        CreateBrigadeAdjustmentsCommand createBrigadeAdjustmentEntitiesCommand = new
        (
            brigadeAdjustmentDataResult,
            companyResult.Value.Id.ToString()
        );

        ErrorOr<bool> createResult = await _mediator.Send(createBrigadeAdjustmentEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch("UpdateBrigadeAdjustment")]
    public async Task<IActionResult> UpdateBrigadeAdjustment(UpdateBrigadeAdjustmentsCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(command);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpDelete("DeleteBrigadeAdjustment")]
    public async Task<IActionResult> DeleteBrigadeAdjustment(DeleteBaseBrigadeAdjustmentCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteBrigadeAdjustmentCommand request = new
        (
            companyResult.Value.Id,
            command.BrigadeAdjustmentId
        );

        ErrorOr<bool> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}