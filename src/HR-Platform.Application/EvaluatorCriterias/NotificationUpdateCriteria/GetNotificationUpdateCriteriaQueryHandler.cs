using ErrorOr;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.NotificationUpdateCriteria;
internal sealed class GetNotificationUpdateCriteriaQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorCriteriaRepository collaboratorCriteriaRepository,
    ICollaboratorCriteriaAnswerRepository collaboratorCriteriaAnswerRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetNotificationUpdateCriteriaQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorCriteriaRepository _collaboratorCriteriaRepository = collaboratorCriteriaRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaRepository));
    private readonly ICollaboratorCriteriaAnswerRepository _collaboratorCriteriaAnswerRepository = collaboratorCriteriaAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaAnswerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<bool>> Handle(GetNotificationUpdateCriteriaQuery query, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("CollaboratorToEvaluators.CreationDate", "CreationDate is not valid");

        if (await _collaboratorRepository.GetByEmailAsync(query.EmailWhoIsLogin) is not Collaborator oldCollaborator)
            return Error.NotFound("CriteriaResult.NotFound", "The company with the provide Id was not found.");

        if (!oldCollaborator.IsEvaluator)
            return Error.Validation("CriteriaResult.IsEvaluator", "The collaborator is not an evaluator.");

        List<CollaboratorCriteria>? criteriasByEvaluator = await _collaboratorCriteriaRepository.GetByEvaluatorIdAsync(oldCollaborator.Id);

        var postionIds = criteriasByEvaluator?.Select(x => new { PositionId = x.Position.Id, x.Position.CriteriasEditionDate, PositionName = x.Position.Name }).Distinct().ToList();

        List<CollaboratorCriteriaAnswer>? collaboratorCriterias = await _collaboratorCriteriaAnswerRepository.GetAllWithoutHistorical(oldCollaborator.Id);

        bool showNotification = false;

        List<CollaboratorCriteriaAnswer> sendEvaluationToHistory = [];

        if (collaboratorCriterias is not null && collaboratorCriterias.Count > 0 && postionIds is not null && postionIds.Count > 0)
        {
            foreach (CollaboratorCriteriaAnswer colCriteria in collaboratorCriterias)
            {
                int indexPositioId = postionIds.FindIndex(x => x.PositionName == colCriteria.Position);

                if (indexPositioId != -1)
                {
                    if (postionIds[indexPositioId].CriteriasEditionDate.Value > colCriteria.EditionDate.Value)
                    {
                        colCriteria.IsInHistorical = true;
                        colCriteria.EditionDate = editionDate;
                        sendEvaluationToHistory.Add(colCriteria);
                        showNotification = true;
                    }
                }
            }

            _collaboratorCriteriaAnswerRepository.UpdateRange(sendEvaluationToHistory);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return showNotification;
    }
}