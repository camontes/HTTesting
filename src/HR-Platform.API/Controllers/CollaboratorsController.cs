using Collaborators.UpdateBankAccount;
using Collaborators.UpdateContract;
using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.ChangeState;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.Create;
using HR_Platform.Application.Collaborators.DeleteEducationFile;
using HR_Platform.Application.Collaborators.GetAllWithEvaluationsInHistorical;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Collaborators.GetByEmail;
using HR_Platform.Application.Collaborators.GetById;
using HR_Platform.Application.Collaborators.GetContractById;
using HR_Platform.Application.Collaborators.ResendInvitation;
using HR_Platform.Application.Collaborators.SendInvitation;
using HR_Platform.Application.Collaborators.UpdateAlreadyLogin;
using HR_Platform.Application.Collaborators.UpdateBasicInformation;
using HR_Platform.Application.Collaborators.UpdateEducationData;
using HR_Platform.Application.Collaborators.UpdateFamilyInformation;
using HR_Platform.Application.Collaborators.UpdateLanguages;
using HR_Platform.Application.Collaborators.UpdateLifePreferences;
using HR_Platform.Application.Collaborators.UpdateLocation;
using HR_Platform.Application.Collaborators.UpdateShowNewFeatures;
using HR_Platform.Application.Collaborators.UpdateSocialSecurity;
using HR_Platform.Application.Collaborators.UpdateSoftSkills;
using HR_Platform.Application.Collaborators.UpdateSuperAdmin;
using HR_Platform.Application.Collaborators.UpdateTechnologyTools;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using HR_Platform.Infrastructure.MailerServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Collaborators")]
public class Collaborators(
    ISender mediator,

    IAmazonCognitoService amazonCognitoService,
    IAmazonS3Service amazonS3Service,
    IMailerService mailerService,
    IStringService stringService
    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IAmazonCognitoService _amazonCognitoService = amazonCognitoService ?? throw new ArgumentNullException(nameof(amazonCognitoService));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));
    private readonly IMailerService _mailerService = mailerService ?? throw new ArgumentNullException(nameof(mailerService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    [HttpGet("GetByEmail")]
    public async Task<IActionResult> GetByEmail()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(new GetCollaboratorByEmailQuery(collaboratorEmail));

        return collaboratorResult.Match(
            collaborator => Ok(collaborator),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetCollaboratorById")]
    public async Task<IActionResult> GetCollaboratorById([FromBody] GetCollaboratorByIdQuery command)
    {

        ErrorOr<CollaboratorsByIdResponse> collaboratorsResult = await _mediator.Send(command);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetAllWithEvaluationsInHistorical")]
    public async Task<IActionResult> GetAllWithEvaluationsInHistorical()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetAllWithEvaluationsInHistoricalQuery query = new(companyResult.Value.Id);

        ErrorOr<List<CollaboratorWithEvaluationsResponse>> collaboratorsResult = await _mediator.Send(query);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetContractByCollaboratorId")]
    public async Task<IActionResult> GetContractByCollaboratorId([FromBody] GetContractByIdQuery command)
    {
        ErrorOr<CollaboratorContractResponse> collaboratorsResult = await _mediator.Send(command);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetEmergencyContactByCollaboratorId")]
    public async Task<IActionResult> GetEmergencyContactByCollaboratorId([FromBody] GetEmergencyContactByIdQuery command)
    {
        ErrorOr<EmergencyContactResponse> collaboratorsResult = await _mediator.Send(command);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetByCompanyIdAndIsPendingInvitation")]
    public async Task<IActionResult> GetById([FromBody] GetBaseCollaboratorsByCompanyIdAndIsPendingInvitationQuery baseCollaboratorsQuery)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        /* IsPendingInvitation */
        /// 1 -> True
        /// 2 -> False
        /// 3 -> Both

        GetCollaboratorsByCompanyIdAndIsPendingInvitationQuery finalCollaboratorsQuery = new
        (
            companyResult.Value.Id,
            baseCollaboratorsQuery.IsPendingInvitation,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
        );

        ErrorOr<CollaboratorsAndCountResponse> collaboratorsResult = await _mediator.Send(finalCollaboratorsQuery);

        return collaboratorsResult.Match(
            collaborators => Ok(collaborators),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] BaseCreateCollaboratorsCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string cvURL = string.Empty;
        string cvName = string.Empty;
        string photoURL = string.Empty;
        string PhotoName = string.Empty;

        string photoNameAux = string.Empty;

        if (command.CvFile != null)
        {
            photoNameAux = command.CvFile.FileName
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

            cvURL = await _amazonS3Service.UploadFile(command.CvFile, "CollaboratorCVsFolder");
            cvName = photoNameAux;

            if (string.IsNullOrEmpty(cvURL))
                return StatusCode(400, new { message = "No se pudo crear el colaborador" });
        }

        GetCollaboratorByEmailDomainQuery getCollaboratorByEmailDomainQuery = new(command.BusinessEmail.ToLower());

        ErrorOr<bool> domainResult = await _mediator.Send(getCollaboratorByEmailDomainQuery);

        if (domainResult.IsError)
            return StatusCode(400, new { message = "Este dominio no está disponible" });


        GetCollaboratorByEmailQuery getCollaboratorByEmailQuery = new(command.BusinessEmail.ToLower());

        ErrorOr<CollaboratorsResponse> collaboratorsResult = await _mediator.Send(getCollaboratorByEmailQuery);

        if (!collaboratorsResult.IsError)
            return StatusCode(400, new { message = "El Colaborador ya existe" });

        string personalEmail = command.BusinessEmail;

        if (command != null && !string.IsNullOrEmpty(command.PersonalEmail))
            personalEmail = command.PersonalEmail;

        CreateCollaboratorsCommand newCreateCollaboratorCommand = new
        (
            companyResult.Value.Id.ToString(),
            collaboratorEmail.ToLower(),

            command.AssignationTypeId,
            command.AssignationId,
            command.DocumentTypeId,
            command.IsAnotherDocumentType,
            command.OtherDocumentType is not null ? command.OtherDocumentType : string.Empty,
            command.PositionId,

            command.BusinessEmail.ToLower(),
            personalEmail,

            _stringService.NameFormat(command.Name),

            command.Document,

            command.ProfessionalCard,
            command.BirthDate,
            command.StreetAddress,

            cvURL, // command.CvFile,
            cvName,
            photoURL, // command.Photo,
            PhotoName, // command.Photo,

            command.SendNotificationsToPersonalEmail,
            true, // IsPendingInvitation

            //command.BirthDate,
            command.EntranceDate
        );

        ErrorOr<Guid> createResult = await _mediator.Send(newCreateCollaboratorCommand);

        ErrorOr<SendInvitationResponse> sendEmailResult = await _mediator.Send(new SendInvitationCommand(command.BusinessEmail, command.PersonalEmail));

        if (sendEmailResult.IsError)
            return sendEmailResult.Match(
                loginCode => Ok(loginCode),
                errors => Problem(errors)
        );

        PasswordMailerDTO passwordMailerDTO = new()
        {
            Subject = "Correo de invitación a la plataforma",
            To = newCreateCollaboratorCommand.BusinessEmail.ToLower(),
            Email = newCreateCollaboratorCommand.BusinessEmail.ToLower(),
            Password = sendEmailResult.Value.Password
        };

        await _mailerService.ResendEmailInvitation(passwordMailerDTO);

        bool isCreated = await _amazonCognitoService.CognitoSignUp(newCreateCollaboratorCommand.BusinessEmail.ToLower(), passwordMailerDTO.Password);

        if (!isCreated)
            return StatusCode(400, new { message = "No se pudo crear el usuario" });

        return createResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("ChangeCollaboratorState")]
    public async Task<IActionResult> ChangeCollaboratorState([FromBody] ChangeCollaboratorStateCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<CollaboratorsResponse> collaboratorsResult = await _mediator.Send(command);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("DeleteEducationFile")]
    public async Task<IActionResult> DeleteEducationFile([FromBody] DeleteEducationFileCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> collaboratorsResult = await _mediator.Send(command);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateBankAccount")]
    public async Task<IActionResult> UpdateBankAccount([FromBody] UpdateBaseBankAccountsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateBankAccountsCommand resultCreateBankResult = new(
            companyEmail,
            command.Id,
            command.CollaboratorId,
            command.BankId,
            command.TypeAccountId,
            command.accountNumberString
        );

        ErrorOr<bool> createResult = await _mediator.Send(resultCreateBankResult);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch("UpdateSuperAdmin")]
    public async Task<IActionResult> UpdateSuperAdmin([FromForm] UpdateBaseSuperAdminCommand command)
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

        string photoNameAux = command.PhotoFile.FileName
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

        if (command.PhotoFile is not null && command.IsChangedPhoto)
        {
            fileURL = await _amazonS3Service.UploadFile(command.PhotoFile, "CollaboratorPhotosFolder");
            fileName = photoNameAux;
        }

        UpdateSuperAdminCommand result = new(
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

    [HttpPatch("UpdateBasicInformation")]
    public async Task<IActionResult> UpdateBasicInformation([FromForm] UpdateBaseBasicInformationCommand command)
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
            companyId => Ok(companyId),
            errors => Problem(errors)
        );

        string logoURL = string.Empty;
        string fileName = string.Empty;

        if (command.PhotoFile != null)
            logoURL = await _amazonS3Service.UploadFile(command.PhotoFile, "CollaboratorPhotosFolder");

        if (!string.IsNullOrEmpty(logoURL))
        {
            GetCollaboratorByIdQuery getCollaboratorByIdQuery = new
            (
                new CollaboratorId(command.Id.Value)
            );

            ErrorOr<CollaboratorsByIdResponse> getCollaboratorResult = await _mediator.Send(getCollaboratorByIdQuery);

            string photoURL = getCollaboratorResult.Value != null && !string.IsNullOrEmpty(getCollaboratorResult.Value.PhotoURL) ? getCollaboratorResult.Value.PhotoURL : string.Empty;

            if (!string.IsNullOrEmpty(photoURL))
            {
                string[] photoURLSplit = photoURL.Split("/");

                string photoToDelete = string.Empty;

                if (photoURLSplit != null)
                {
                    if (photoURLSplit.Length > 1)
                        photoToDelete = photoURLSplit[^2] + "/" + photoURLSplit[^1];
                }

                bool isDeletedFile = await _amazonS3Service.DeleteFile(photoToDelete);
            }
        }

        UpdateBasicInformationCommand updateBasicInformationCommand = new
        (
            collaboratorEmail,
            command.Id.Value,

            command.PhotoFile,

            logoURL,

            fileName
        );

        ErrorOr<UpdateBasicInformationResponse> collaboratorsResult = await _mediator.Send(updateBasicInformationCommand);

        collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );

        if (string.IsNullOrEmpty(logoURL))
            return StatusCode(200, new { message = "Foto eliminada exitosamente", collaboratorsResult });

        return StatusCode(201, new { message = "Foto actualizada exitosamente", collaboratorsResult });
    }

    [HttpPatch("UpdateEducationData")]
    public async Task<IActionResult> UpdateEducationData([FromBody] UpdateBaseEducationDataCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateEducationDataCommand updatedResponse = new(
          collaboratorEmail,
          command.Id.Value,
          companyResult.Value.Id.ToString(),
          command.EducationLevelId,
          command.ProfessionalAdviceId,
          command.ProfessionalCard
          );

        ErrorOr<UpdateCollaboratorResponse> updateResponse = await _mediator.Send(updatedResponse);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateLanguage")]
    public async Task<IActionResult> UpdateLanguage([FromBody] UpdateBaseLanguageCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateLanguageCommand resultLanguage = new(
            collaboratorEmail,
            command.CollaboratorId,
            command.LanguageList
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(resultLanguage);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateContract")]
    public async Task<IActionResult> UpdateContract([FromBody] UpdateBaseContractsCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateContractsCommand resultLanguage = new(
            collaboratorEmail,
            companyResult.Value.Id,
            command.CollaboratorId,
            command.Id,
            command.PositionId,
            command.AssignationTypeId,
            command.AssignationId,
            command.ContractTypeId,
            command.Salary,
            command.CurrencyTypeId,
            command.Arl,
            command.Bonus
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(resultLanguage);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateTechnologyTool")]
    public async Task<IActionResult> UpdateTechnologyTool([FromBody] UpdateBaseTechnologyToolCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateTechnologyToolCommand resultTechnologyTool = new(
            collaboratorEmail,
            command.CollaboratorId,
            command.TechnologyToolList
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(resultTechnologyTool);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateSoftSkill")]
    public async Task<IActionResult> UpdateSoftSkill([FromBody] UpdateBaseSoftSkillCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateSoftSkillCommand resultSoftSkill = new(
            collaboratorEmail,
            command.CollaboratorId,
            command.SoftSkillList
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(resultSoftSkill);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateLifePreference")]
    public async Task<IActionResult> UpdateLifePreference([FromBody] UpdateBaseLifePreferenceCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateLifePreferenceCommand resultLifePreferences = new(
            collaboratorEmail,
            command.CollaboratorId,
            command.LifePreferenceList
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(resultLifePreferences);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateLocation")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateBaseLocationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateLocationCommand updatedResponse = new(
          collaboratorEmail,
          command.CollaboratorId.Value,
          companyResult.Value.Id.ToString(),
          command.Birthdate,
          command.Country,
          command.Department,
          command.City,
          command.EconomicLevelId,
          command.LocationAddress,
          command.PhoneNumber,
          command.PostalCode
          );

        ErrorOr<UpdateLocationResponse> updateResponse = await _mediator.Send(updatedResponse);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );

    }

    [HttpPatch("UpdateSocialSecurity")]
    public async Task<IActionResult> UpdateSocialSecurity([FromBody] UpdateBaseSocialSecurityCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateSocialSecurityCommand updatedResponse = new(
          collaboratorEmail,
          command.CollaboratorId,
          companyResult.Value.Id.ToString(),
          command.PensionId,
          command.SeveranceBenefitId,
          command.HealthEntityId
          );

        ErrorOr<UpdateSocialSecurityResponse> updateResponse = await _mediator.Send(updatedResponse);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );

    }

    [HttpPatch("UpdateEmergencyContact")]
    public async Task<IActionResult> UpdateEmergencyContact([FromBody] UpdateThBaseEmergencyContactCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        UpdateBaseEmergencyContactCommand commandTH = new(
            collaboratorEmail,
            command.CollaboratorId,
            command.EmergencyContactsList
        );

        ErrorOr<EmergencyContactResponse> updateResponse = await _mediator.Send(commandTH);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );

    }

    [HttpPatch("UpdateFamilyInformation")]
    public async Task<IActionResult> UpdateFamilyInformation([FromBody] UpdateBaseFamilyInformationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );


        UpdateFamilyInformationCommand updateFamilyInformationCommand = new
       (
           collaboratorEmail,
           command.CollaboratorId,

           command.MaritalStatusId,

           command.FamilyMembersNumber,
           command.ChildrenNumber,

           command.FamilyComposition,
           command.Children
       );

        ErrorOr<UpdateFamilyInformationResponse> collaboratorsResult = await _mediator.Send(updateFamilyInformationCommand);

        return collaboratorsResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("ResendInvitation")]
    public async Task<IActionResult> ResendInvitation(ResendInvitationRequest resendInvitationRequest)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<ResendInvitationResponse> resendEmailResult = await _mediator.Send(new ResendInvitationCommand(resendInvitationRequest.BusinessEmail, resendInvitationRequest.PersonalEmail));

        if (resendEmailResult.IsError)
            return resendEmailResult.Match(
                loginCode => Ok(loginCode),
                errors => Problem(errors)
        );

        bool isChangedPassword = await _amazonCognitoService.CognitoAdminChangePassword(resendInvitationRequest.BusinessEmail, resendEmailResult.Value.Password);

        if (!isChangedPassword)
        {
            return StatusCode(400, new { message = "No se pudo realizar el cambio de contraseña" });
        }

        PasswordMailerDTO passwordMailerDTO = new()
        {
            Subject = "Correo de invitación a la plataforma",
            To = resendInvitationRequest.BusinessEmail,
            Email = resendInvitationRequest.BusinessEmail,
            Password = resendEmailResult.Value.Password
        };

        await _mailerService.ResendEmailInvitation(passwordMailerDTO);

        if (resendInvitationRequest != null && !string.IsNullOrEmpty(resendInvitationRequest.PersonalEmail))
        {
            passwordMailerDTO = new()
            {
                Subject = "Correo de invitación a la plataforma",
                To = resendInvitationRequest.PersonalEmail,
                Email = resendInvitationRequest.PersonalEmail,
                Password = resendEmailResult.Value.Password
            };

            await _mailerService.ResendEmailInvitation(passwordMailerDTO);
        }

        return resendEmailResult.Match(
            password => Ok(string.Empty),
            errors => Problem(errors)
        );
    }

    [HttpPatch("ChangePasswordFromAccount")]
    public async Task<IActionResult> ChangePasswordFromAccount(ChangePasswordFromAccountRequest changePasswordRequest)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string changePasswordMessage = await _amazonCognitoService.CognitoChangePassword(collaboratorEmail, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

        bool isChangedPassword = false;

        if (changePasswordMessage.ToLower().Trim() == "change successfully")
            isChangedPassword = true;

        if (!isChangedPassword)
        {
            return StatusCode(400, new { message = changePasswordMessage });
        }

        return Ok(new { message = "Contraseña actualizada exitosamente", collaboratorEmail });
    }

    [AllowAnonymous]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        string changePasswordMessage = await _amazonCognitoService.CognitoChangePassword(changePasswordRequest.BusinessEmail, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

        bool isChangedPassword = false;

        if (changePasswordMessage.ToLower().Trim() == "change successfully")
            isChangedPassword = true;

        if (!isChangedPassword)
        {
            return StatusCode(400, new { message = changePasswordMessage });
        }

        GetCollaboratorByEmailQuery getCollaboratorByEmailQuery = new
        (
            changePasswordRequest.BusinessEmail
        );

        ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(getCollaboratorByEmailQuery);

        UpdateAlreadyLoginCommand updateAlreadyLoginCommand = new
        (
            collaboratorResult.Value.Id,
            true // AlreadyLogin
        );

        collaboratorResult = await _mediator.Send(updateAlreadyLoginCommand);

        return Ok(new { message = "Contraseña actualizada exitosamente", collaboratorResult.Value });
    }

    [HttpGet("ChangeShowFeatureToFalse")]
    public async Task<IActionResult> ChangeShowFeatureToFalse()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetCollaboratorByEmailQuery getCollaboratorByEmailQuery = new
        (
            collaboratorEmail
        );

        ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(getCollaboratorByEmailQuery);

        UpdateShowNewFeaturesCommand updateShowNewFeaturesCommand = new
        (
            collaboratorResult.Value.Id,
            false // ShowNewFeatures
        );

        collaboratorResult = await _mediator.Send(updateShowNewFeaturesCommand);



        return Ok(new { message = "Visualización de Nuevas Características actualizada exitosamente", collaboratorResult.Value });
    }
}