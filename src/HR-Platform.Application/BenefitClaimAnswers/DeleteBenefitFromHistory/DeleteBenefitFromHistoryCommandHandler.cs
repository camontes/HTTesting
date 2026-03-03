using ErrorOr;
using HR_Platform.Application.BenefitClaimAnswers.Common;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace BenefitClaimAnswers.DeleteBenefitFromHistory;

internal sealed class GetBenefitHistoryNameQueryHandler(
    IBenefitClaimAnswerRepository benefitClaimAnswerRepository,
    INotificationTypeRepository notificationTypeRepository,
    INotificationRepository notificationRepository,
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBenefitFromHistoryCommand, ErrorOr<List<CollaboratorEmailResponse>>>
{
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository ?? throw new ArgumentNullException(nameof(notificationTypeRepository));
    private readonly IBenefitClaimAnswerRepository _benefitClaimAnswerRepository = benefitClaimAnswerRepository ?? throw new ArgumentNullException(nameof(benefitClaimAnswerRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<List<CollaboratorEmailResponse>>> Handle(DeleteBenefitFromHistoryCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BenefitClaimAnswer.CreationDate", "CreationDate is not valid");

        List<BenefitClaimAnswer>? benefitClaimAnswer = await _benefitClaimAnswerRepository.GetByBenefitNameAsync(new CompanyId(command.CompanyId), command.BenefitName);
        List<CollaboratorEmailResponse> result = [];

        if (benefitClaimAnswer is not null && benefitClaimAnswer.Count > 0)
        {
            Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

            #region Get Emails who have applied to benefits 
            var uniqueNames = benefitClaimAnswer
             .GroupBy(e => e.Collaborator.BusinessEmail.Value)
             .Select(g => new { BusinessEmail = g.Key })
             .ToList();

            foreach (var colaborator in uniqueNames)
            {
                CollaboratorEmailResponse temp = new
                (
                    colaborator.BusinessEmail
                );
                result.Add(temp);
            }
            #endregion

            #region Switch to deleted to Benefit
            foreach (BenefitClaimAnswer item in benefitClaimAnswer)
            {
                item.HasDeleted = true;
                item.NameWhoDeletedBenefitClaim = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty;
                item.EmailWhoDeletedBenefitClaim = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.BusinessEmail.Value : string.Empty;
                item.DeletedDate = creationDate;
            }
            _benefitClaimAnswerRepository.UpdateRange(benefitClaimAnswer);
            #endregion

            #region Notification
            NotificationType? notificationType = await _notificationTypeRepository.GetByIdAsync(new NotificationTypeId(4)); // 4 -> Notification Deleted from history

            if (notificationType is not null)
            {
                try
                {
                    foreach (BenefitClaimAnswer collaboratorBenefitClaim in benefitClaimAnswer)
                    {
                        Notification notification = new
                        (
                            new NotificationId(Guid.NewGuid()),

                            notificationType.Message.Replace("@1", "<em>" + collaboratorBenefitClaim.BenefitName + "</em>").Replace("@2", "<em>" + collaboratorBenefitClaim.ReferenceNumber + "</em>"),
                            notificationType.MessageEnglish.Replace("@1", "<em>" + collaboratorBenefitClaim.BenefitName + "</em>").Replace("@2", "<em>" + collaboratorBenefitClaim.ReferenceNumber + "</em>"),

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
                            creationDate
                        );

                        _notificationRepository.Add(notification);

                    }

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    return Error.Forbidden("Error Deleting benefit with that name");
                }
            }
            #endregion

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return result;
    }
}