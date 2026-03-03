using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.CreateEducations;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Education")]
public class Educations(

    ISender mediator,
    IAmazonS3Service amazonS3Service
    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UpdateBaseEducationCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string educationFileURL = string.Empty;
        string educationFileName = string.Empty;

        if (command.EducationFile != null)
        {
            string fileNameAux = command.EducationFile.FileName
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

            educationFileURL = await _amazonS3Service.UploadFile(command.EducationFile, "CollaboratorEducationFilesFolder");
            educationFileName = fileNameAux;

            if (string.IsNullOrEmpty(educationFileURL))
                return StatusCode(400, new { message = "File could not be loaded  " });
        }

        UpdateEducationCommand resultEducation = new(
           companyEmail,
           command.CollaboratorId,
           command.InstitutionName,
           command.ProfessionId,
           command.OtherProfessionName,
           command.EducationLevelId,
           command.StudyTypeId,
           command.IsCertificated,
           command.StudyAreaId,
           command.IsCompletedStudy,
           command.StartEducationDate,
           command.EndEducationDate,
           command.EducationStageId,
           !string.IsNullOrEmpty(educationFileURL) ? educationFileURL : string.Empty,
           !string.IsNullOrEmpty(educationFileName) ? educationFileName : string.Empty
        );


        ErrorOr<bool> createResult = await _mediator.Send(resultEducation);

        return createResult.Match(
           educations => Ok(educations),
           errors => Problem(errors)
       );

    }
}