using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.GetByEmail;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Permissions.ValidateMultipleStrings;
using HR_Platform.Domain.Roles;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Permissions")]
public class Permissions(
    IPermissionsValidatorService permissionsValidatorService,

    ISender mediator
    ) : ApiController
{
    private readonly IPermissionsValidatorService _permissionsValidatorService = permissionsValidatorService;

    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("ValidateMultipleStrings")]
    public async Task<IActionResult> ValidateMultipleStrings(ValidateMultipleStringsQuery query)
    {
        try
        {
            Token token = new();

            string collaboratorEmail = token.GetEmailFromToken(Request);

            GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

            ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

            companyResult.Match(
                company => Ok(company),
                errors => Problem(errors)
            );

            ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(new GetCollaboratorByEmailQuery(collaboratorEmail));

            collaboratorResult.Match(
                collaborator => Ok(collaborator),
                errors => Problem(errors)
            );

            Dictionary<string, bool> dictionaryValidations =
                await _permissionsValidatorService.IsValidatedPermissionsMultiple(new RoleId(collaboratorResult.Value.RoleId), query.PermissionStrings);

            return Ok(new { message = "Permisos validatos exitosamente", obj =  dictionaryValidations });
        }
        catch(Exception)
        {
            return StatusCode(500, new { message = "No se pudieron validar los permisos" });
        }
    }
}