using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.Update;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.MasterUsers.Common;
using HR_Platform.Application.MasterUsers.GetByEmail;
using HR_Platform.Application.MasterUsers.Update;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("MasterUsers")]
public class MasterUsers(
    IAmazonS3Service amazonS3Service,
    ISender mediator
    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpGet("GetByEmail")]
    public async Task<IActionResult> GetByEmail()
    {
        Token token = new();

        string masterUserEmail = token.GetEmailFromToken(Request);

        ErrorOr<MasterUsersResponse> masterUserResult = await _mediator.Send(new GetMasterUserByEmailQuery(masterUserEmail));

        return masterUserResult.Match(
            collaborator => Ok(collaborator),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateMasterUser")]
    public async Task<IActionResult> UpdateMasterUser([FromForm] UpdateBaseMasterUserCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
        string fileURL = string.Empty;
        string fileName = string.Empty;

        if (command.PhotoFile is not null && command.IsChangedPhoto)
        {
            string fileNameAux = command.PhotoFile.FileName
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

            fileURL = await _amazonS3Service.UploadFile(command.PhotoFile, "CollaboratorPhotosFolder");
            fileName = fileNameAux;
        }

        UpdateMasterUserCommand result = new(
            companyEmail,
            command.Name,
            command.Phone,
            command.IsChangedPhoto,
            fileURL,
            fileName
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }
}