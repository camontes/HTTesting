using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Benefits;
using MediatR;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using Amazon.Auth.AccessControlPolicy;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Application.Benefits.DeleteBenefitQuery;

internal sealed class DeleteBenefitQueryHandler(
    IBenefitRepository benefitRepository,
    ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,
    INotificationRepository notificationRepository,
    INotificationTypeRepository notificationTypeRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBenefitQuery, ErrorOr<bool>>
{
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository ?? throw new ArgumentNullException(nameof(notificationTypeRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteBenefitQuery query, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BenefitClaimAnswer.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate editionDate)
            return Error.Validation("BenefitClaimAnswer.EditionDate", "EditionDate is not valid");

        if (await _benefitRepository.GetByIdAsync(new BenefitId(query.Id)) is not Benefit oldBenefit)
            return Error.NotFound("Benefit.NotFound", "The Benefit with the provide Id was not found.");
        
        try
        {
            NotificationType? notificationType = await _notificationTypeRepository.GetByIdAsync(new NotificationTypeId(3)); // 3 -> Notification Deleted

            if (notificationType is not null)
            {
                try
                {
                    List<CollaboratorBenefitClaim>? collaboratorBenefitClaims = await _collaboratorBenefitClaimRepository.GetByBenefitIdAsync(oldBenefit.Id);

                    if(collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Count > 0)
                    {
                        foreach (CollaboratorBenefitClaim collaboratorBenefitClaim in collaboratorBenefitClaims)
                        {
                            Notification notification = new
                            (
                                new NotificationId(Guid.NewGuid()),

                                notificationType.Message.Replace("@1", "<em>" + oldBenefit.Name + "</em>").Replace("@2", "<em>" + collaboratorBenefitClaim.ReferenceNumber + "</em>"),
                                notificationType.MessageEnglish.Replace("@1", "<em>" + oldBenefit.Name + "</em>").Replace("@2", "<em>" + collaboratorBenefitClaim.ReferenceNumber + "</em>"),

                                "", // SourceEmail
                                "", // SourceName
                                "", // SourceInitials

                                "https://hr-platform.s3.us-east-1.amazonaws.com/DefaultIconsDev/Bell.png",

                                false, // IsRead

                                true, // IsEditable
                                true, // IsDeleteable

                                collaboratorBenefitClaim.CollaboratorId,
                                notificationType.Id,

                                creationDate,
                                editionDate
                            );

                            _notificationRepository.Add(notification);
                        }
                    }

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            _benefitRepository.Delete(oldBenefit);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}