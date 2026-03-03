using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BankAccounts.Common;
using HR_Platform.Application.BankAccounts.GetByCompanyId;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BankAccount")]
public class BankAccounts(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetBankAccountsById")]
    public async Task<IActionResult> GetBankAccountsByCompanyId([FromBody] GetBankAccountByIdQuery BankAccountQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<BankAccountsResponse> result = await _mediator.Send(BankAccountQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}