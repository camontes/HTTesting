using ErrorOr;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.AddNewLoginCode;
using HR_Platform.Application.Collaborators.AddNewRecoveryCode;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.IsValidRecoveryCode;
using HR_Platform.Application.Collaborators.ValidateLoginCode;
using HR_Platform.Application.Common;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using HR_Platform.Infrastructure.MailerServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Login")]
public class Login(
    ISender mediator,

    IAmazonCognitoService amazonCognitoService,
    IMailerService mailerService
    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IAmazonCognitoService _amazonCognitoService = amazonCognitoService ?? throw new ArgumentNullException(nameof(amazonCognitoService));
    private readonly IMailerService _mailerService = mailerService ?? throw new ArgumentNullException(nameof(mailerService));

    [AllowAnonymous]
    [HttpPost("GetRandomToken")]
    public async Task<IActionResult> GetRandomToken(LoginCodeRequest loginCodeRequest)
    {
        ErrorOr<LoginCodeResponse> loginCodeResult = await _mediator.Send(new AddNewLoginCodeCommand(loginCodeRequest.Email));

        if (loginCodeResult.IsError)
            return loginCodeResult.Match(
                loginCode => Ok(loginCode),
                errors => Problem(errors)
            );

        TokenMailerDTO tokenMailerDTO = new()
        {
            Subject = "Correo de verificación",
            To = loginCodeRequest.Email.ToLower(),
            Token = loginCodeResult.Value.LoginCode
        };

        await _mailerService.SendLoginCodeMail(tokenMailerDTO);

        return loginCodeResult.Match(
            loginCode => Ok(loginCode),
            errors => Problem(errors)
        );
    }

    [AllowAnonymous]
    [HttpPost("ValidCode")]
    public async Task<IActionResult> ValidateLoginCode(ValidateLoginCodeRequest validateLoginCodeRequest)
    {
        try
        {
            ErrorOr<ValidateLoginCodeResponse> validateLoginCodeResult = await _mediator.Send(new ValidateLoginCodeCommand(validateLoginCodeRequest.LoginCode, validateLoginCodeRequest.Email.ToLower()));

            if (validateLoginCodeResult.IsError)
                return validateLoginCodeResult.Match(
                    loginCode => Ok(loginCode),
                    errors => Problem(errors)
            );

            return validateLoginCodeResult.Match(
                loginCode => Ok(loginCode),
                errors => Problem(errors)
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost("SendRecoverPasswordToken")]
    public async Task<IActionResult> SendRecoverPasswordToken(LoginCodeRequest loginCodeRequest)
    {
        ErrorOr<RecoveryCodeResponse> recoveryCodeResult = await _mediator.Send(new AddNewRecoveryCodeCommand(loginCodeRequest.Email.ToLower()));

        if (recoveryCodeResult.IsError)
            return recoveryCodeResult.Match(
                recoveryCode => Ok(recoveryCode),
                errors => Problem(errors)
            );

        TokenMailerDTO tokenMailerDTO = new()
        {
            Subject = "Correo de recuperación de contraseña",
            To = loginCodeRequest.Email.ToLower(),
            Token = recoveryCodeResult.Value.RecoveryCode
        };

        await _mailerService.SendRecoveryPasswordMail(tokenMailerDTO);

        return recoveryCodeResult.Match(
            recoveryCode => Ok(recoveryCode),
            errors => Problem(errors)
        );
    }

    [AllowAnonymous]
    [HttpPost("RecoverPassword")]
    public async Task<IActionResult> RecoverPassword(RecoveryPasswordRequest recoveryPasswordRequest)
    {
        GetCompanyByEmailQuery emailQuery = new(recoveryPasswordRequest.BusinessEmail.ToLower());

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
        errors => Problem(errors)
        );

        IsValidRecoveryCodeQuery isValidRecoveryCodeQuery = new
        (
            Email.Create(recoveryPasswordRequest.BusinessEmail),
            recoveryPasswordRequest.RecoveryCode
        );

        ErrorOr<BooleanExistsResponse> codeResult = await _mediator.Send(isValidRecoveryCodeQuery);

        if(codeResult.Value == null || !codeResult.Value.Exists)
            return StatusCode(400, new { message = "Invalid verification code provided, please try again." });

        string changePasswordMessage = await _amazonCognitoService.
           CognitoChangePassword(recoveryPasswordRequest.BusinessEmail.ToLower(), recoveryPasswordRequest.OldPassword, recoveryPasswordRequest.NewPassword);

        bool isChangedPassword = false;

        if (changePasswordMessage.ToLower().Trim() == "change successfully")
            isChangedPassword = true;

        if (isChangedPassword)
        {
            return Ok(new { message = "Contraseña actualizada exitosamente" });
        }

        return StatusCode(400, new { message = changePasswordMessage });
    }
}