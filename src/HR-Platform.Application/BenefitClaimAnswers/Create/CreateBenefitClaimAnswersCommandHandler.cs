using ErrorOr;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BenefitClaimAnswers.Create;

internal sealed class CreateBenefitClaimAnswersCommandHandler
(
    IBenefitClaimAnswerRepository benefitClaimAnswerRepository,
    ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,
    ICollaboratorRepository collaboratorRepository,
    INotificationRepository notificationRepository,
    INotificationTypeRepository notificationTypeRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<CreateBenefitClaimAnswersCommand, ErrorOr<bool>>
{
    private readonly IBenefitClaimAnswerRepository _benefitClaimAnswerRepository = benefitClaimAnswerRepository ?? throw new ArgumentNullException(nameof(benefitClaimAnswerRepository));
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(collaboratorBenefitClaimRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository ?? throw new ArgumentNullException(nameof(notificationTypeRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBenefitClaimAnswersCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BenefitClaimAnswer.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate editionDate)
            return Error.Validation("BenefitClaimAnswer.EditionDate", "EditionDate is not valid");

        if (await _collaboratorBenefitClaimRepository.GetByIdAsync(new CollaboratorBenefitClaimId(command.BenefitClaimId)) is not CollaboratorBenefitClaim collaboratorBenefitClaim)
            return Error.Validation("BenefitClaimAnswer.BenefitClaimId", "BenefitClaim with the provide Id was not found");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.CollaboratorWhoManagedEmail);


        BenefitClaimAnswer benefitClaimAnswer = new
        (
            new BenefitClaimAnswerId(Guid.NewGuid()),
            collaboratorBenefitClaim.CompanyId,

            collaboratorBenefitClaim.CollaboratorId,

            collaboratorBenefitClaim.Benefit.Name,
            command.Details,
            collaboratorBenefitClaim.ReferenceNumber,
            command.IsBenefitAccepeted,

            collaboratorBenefitClaim.Benefit.IsAvailableForAll,
            collaboratorBenefitClaim.Benefit.MinimumMonthsConstraint,
            collaboratorBenefitClaim.Benefit.IsAnotherContraint,
            collaboratorBenefitClaim.Benefit.AnotherContraint,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name: string.Empty,
            command.CollaboratorWhoManagedEmail,

            collaboratorBenefitClaim.CreationDate, //ManagementDate

            false, // HasDeleted 
            "", // NameWhoDeletedBenefitClaim 
            "", // EmailWhoDeletedBenefitClaim 
            creationDate, // DeletedDate 

        true, // IsEditable
            true, // IsDeleteable

            creationDate,
            editionDate
        );

        _benefitClaimAnswerRepository.Add(benefitClaimAnswer);

        try
        {
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            NotificationType? notificationType = await _notificationTypeRepository.GetByIdAsync(new NotificationTypeId(1));

            if (notificationType is not null)
            {
                try
                {
                    string stateText = string.Empty;
                    string stateTextEnglish = string.Empty;

                    if (command.IsBenefitAccepeted)
                    {
                        stateText = "Aprobada";
                        stateTextEnglish = "Approved";
                    }
                    else
                    {
                        stateText = "Cancelada";
                        stateTextEnglish = "Cancelled";
                    }

                    Notification notification = new
                    (
                        new NotificationId(Guid.NewGuid()),

                        notificationType.Message.Replace("@1", "<em>" + collaboratorBenefitClaim.Benefit.Name + " " + collaboratorBenefitClaim.ReferenceNumber + "</em>").Replace("@2", "<em>" + stateText + "</em>"),
                        notificationType.MessageEnglish.Replace("@1", "<em>" + collaboratorBenefitClaim.Benefit.Name + " " + collaboratorBenefitClaim.ReferenceNumber + "</em>").Replace("@2", "<em>" + stateTextEnglish + "</em>"),

                        "",
                        "",
                        "",
                        "https://hr-platform.s3.us-east-1.amazonaws.com/DefaultIconsDev/Wrench.png",

                        false, // IsRead

                        true, // IsEditable
                        true, // IsDeleteable

                        collaboratorBenefitClaim.CollaboratorId,
                        notificationType.Id,

                        creationDate,
                        editionDate
                    );

                    _notificationRepository.Add(notification);
                    _collaboratorBenefitClaimRepository.Delete(collaboratorBenefitClaim);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}