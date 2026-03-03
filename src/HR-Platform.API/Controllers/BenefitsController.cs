using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Benefits.Common;
using HR_Platform.Application.Benefits.Create;
using HR_Platform.Application.Benefits.DeleteBenefitQuery;
using HR_Platform.Application.Benefits.GetByCompanyId;
using HR_Platform.Application.Benefits.Update;
using HR_Platform.Application.Benefits.UpdateIsVisible;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Benefits")]
public class Benefit(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetBenefitsByCompanyId")]
    public async Task<IActionResult> GetBenefitsByCompanyId([FromBody] GetBaseBenefitByCompanyIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetBenefitByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id,
            baseQuery.IsVisible
        );

        ErrorOr<List<BenefitFileResponse>> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateBenefit")]
    public async Task<IActionResult> Create([FromForm] CreateBaseBenefitCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string fileURL = string.Empty;
        string fileName = string.Empty;
        string imageURL = string.Empty;
        string imageName = string.Empty;

        if (command.File != null)
        {
            string fileNameAux = command.File.FileName
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

            fileURL = await _amazonS3Service.UploadFile(command.File, "BenefitsFolder");
            fileName = fileNameAux;
        }

        if (command.Image != null)
        {
            string imageNameAux = command.Image.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.Image, "BenefitsFolder");
            imageName = imageNameAux;
        }

        CreateBenefitsCommand result = new(
            collaboratorEmail,
            companyResult.Value.Id,
            command.Name,
            command.Description,
            command.IsAvailableForAll,
            command.MinimumMonthsConstraint,
            command.IsAnotherContraint,
            !string.IsNullOrEmpty(command.AnotherContraint) ? command.AnotherContraint : string.Empty,
            fileName,
            fileURL,
            imageName,
            imageURL,
            command.IsAddedButton,
            !string.IsNullOrEmpty(command.ButtonName) ? command.ButtonName : string.Empty,
            command.IsSurveyInclude
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateBenefit")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseBenefitCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string fileURL = string.Empty;
        string fileName = string.Empty;
        string imageURL = string.Empty;
        string imageName = string.Empty;

        if (command.File != null && command.IsChangedFile)
        {
            string fileNameAux = command.File.FileName
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

            fileURL = await _amazonS3Service.UploadFile(command.File, "BenefitsFolder");
            fileName = fileNameAux;
        }

        if (command.Image != null && command.IsChangedImage)
        {
            string imageNameAux = command.Image.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.Image, "BenefitsFolder");
            imageName = imageNameAux;
        }

        UpdateBenefitsCommand result = new(
            collaboratorEmail,
            companyResult.Value.Id, //CompanyId
            command.BenefitId,
            command.Name,
            command.Description,
            command.IsAvailableForAll,
            command.MinimumMonthsConstraint,
            command.IsAnotherContraint,
            command.AnotherContraint,
            command.IsChangedFile,
            fileName,
            fileURL,
            command.IsChangedImage,
            imageName,
            imageURL,
            command.IsAddedButton,
            command.ButtonName,
            command.IsSurveyInclude
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpDelete("DeleteBenefit")]
    public async Task<IActionResult> DeleteBenefit([FromBody] DeleteBenefitQuery baseQuery)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("UpdateIsVisibleBenefitById")]
    public async Task<IActionResult> UpdateIsVisibleBenefitById([FromBody] UpdateIsVisibleBenefitCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseCommand);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}