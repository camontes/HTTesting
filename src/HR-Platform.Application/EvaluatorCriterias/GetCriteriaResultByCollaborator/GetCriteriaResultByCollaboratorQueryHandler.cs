using ErrorOr;
using HR_Platform.Application.EvaluatorCriterias.Common;
using HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByEvaluator;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Positions;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.EvaluatorCriterias.GetCriteriaResultByCollaborator;

internal sealed class GetCriteriaResultByCollaboratorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IPositionRepository positionRepository,
    ICollaboratorCriteriaRepository collaboratorCriteriaRepository,
    ITimeFormatService timeFormatService,
    IStringService stringService,
    ICalculateTimeDifference calculateTimeDifference


    ) : IRequestHandler<GetCriteriaResultByCollaboratorQuery, ErrorOr<List<CriteriaResultByCollaboratorResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly ICollaboratorCriteriaRepository _collaboratorCriteriaRepository = collaboratorCriteriaRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<CriteriaResultByCollaboratorResponse>>> Handle(GetCriteriaResultByCollaboratorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.EmailWhoIsLogin) is not Collaborator evaluatorLogIn)
            return Error.NotFound("CriteriaResult.NotFound", "The company with the provide Id was not found.");

        if (!evaluatorLogIn.IsEvaluator)
            return Error.Validation("CriteriaResult.IsEvaluator", "The collaborator is not an evaluator.");

        List<CriteriaResultByCollaboratorResponse> response = [];
        List<CriteriaResultByCollaboratorResponse> responseNoHistory = [];

        List<CollaboratorCriteria>? collaboratorCriterias = await _collaboratorCriteriaRepository.GetByCollaboratorIdAndEvaluatorIdAsync(new CollaboratorId(query.CollaboratorId), evaluatorLogIn.Id);


        if (collaboratorCriterias != null && collaboratorCriterias.Count > 0)
        {
            if (await _positionRepository.GetByIdAsync(collaboratorCriterias[0].PositionId) is not Position oldPosition)
                return Error.NotFound("CriteriaResult.NotFound", "The Position with the provide Id was not found.");

            var collaboratorCriteriasGroup = collaboratorCriterias
            .SelectMany(c => c.CollaboratorCriteriaAnswers
                .GroupBy(a => a.ReferenceNumber)
                .Select(g => new CollaboratorCriteria(
                    c.Id,
                    c.CollaboratorEvaluatedId,
                    c.EvaluatorId,
                    c.PositionId,
                    c.IsEditable,
                    c.IsDeleteable,
                    c.CreationDate,
                    c.EditionDate
                )
                {
                    CollaboratorCriteriaAnswers = [.. g], // Solo las respuestas del grupo
                    CollaboratorEvaluated = c.CollaboratorEvaluated, // Mantiene la relación
                    Evaluator = c.Evaluator,
                    Position = c.Position
                })
            )
            .ToList();

            foreach (CollaboratorCriteria criteria in collaboratorCriteriasGroup)
            {
                List<CriteriaAnswer> answerObjetives = [];
                List<CriteriaAnswer> answerSubjetives = [];

                double ScoreTotalObjetive = 0;
                double ScoreTotalSubjetive = 0;

                bool isInHistory = !query.IsInHistory && criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value > oldPosition.CriteriasEditionDate.Value;
                string collaboratorCriteriaAnswerId = string.Empty;

                foreach (CollaboratorCriteriaAnswer answer in criteria.CollaboratorCriteriaAnswers)
                {
                    CriteriaAnswer tempAnswer = new
                    (
                        answer.CriteriaName,
                        answer.CriteriaNameEnglish,
                        answer.CriteriaPercentage,
                        answer.CriteriaScorePercentage,
                        answer.CriteriaScoreIndexAnswerr == 1 ? "R"
                        : answer.CriteriaScoreIndexAnswerr == 2
                        ? "Y" : answer.CriteriaScoreIndexAnswerr == 3 ? "O" : "G",
                        answer.CriteriaScoreIndexAnswerr,
                        answer.CriteriaScoreName,
                        answer.CriteriaScoreNameEnglish
                    );

                    if (answer.EvaluationCriteriaTypeId.Value == 1)
                    {
                        answerObjetives.Add(tempAnswer);
                        ScoreTotalObjetive += (double)answer.CriteriaScorePercentage * answer.CriteriaPercentage / 100;
                    }
                    else if (answer.EvaluationCriteriaTypeId.Value == 2)
                    {
                        answerSubjetives.Add(tempAnswer);
                        ScoreTotalSubjetive += (double)answer.CriteriaScorePercentage * answer.CriteriaPercentage / 100;
                    }
                    if (string.IsNullOrEmpty(collaboratorCriteriaAnswerId))
                    {
                       collaboratorCriteriaAnswerId = answer.Id.Value.ToString();
                    }
                }

                int generalScoreTotalObjetive = criteria.CollaboratorCriteriaAnswers[0].GeneralObjetiveCriteriaPercentage;
                CriteriaResult criteriaObjetiveResultTemp = new
                (
                    generalScoreTotalObjetive,
                    Math.Round((ScoreTotalObjetive * generalScoreTotalObjetive / 100), 2),
                    Math.Round(ScoreTotalObjetive, 2),
                    answerObjetives
                );

                int generalScoreTotalSubjetive = criteria.CollaboratorCriteriaAnswers[0].GeneralSubjetiveCriteriaPercentage;
                CriteriaResult criteriaSubjetiveResultTemp = new
                (
                    generalScoreTotalSubjetive,
                    Math.Round((ScoreTotalSubjetive * generalScoreTotalSubjetive / 100), 2),
                    Math.Round(ScoreTotalSubjetive, 2),
                    answerSubjetives
                );

                double generalScoreResult = Math.Round(criteriaObjetiveResultTemp.CriteriaPercentageTotal + criteriaSubjetiveResultTemp.CriteriaPercentageTotal,2);

                CriteriaResultByCollaboratorResponse temp = new
                (
                    collaboratorCriteriaAnswerId,
                    criteria.Id.Value,
                    criteria.CollaboratorCriteriaAnswers[0].ReferenceNumber,
                    generalScoreResult,
                    generalScoreResult > 79 ? "G" : generalScoreResult > 49 ? "Y" : "R",
                    criteria.Evaluator.Name,
                    _stringService.GetInitials(criteria.Evaluator.Name),
                    criteria.Evaluator.Photo,
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Agregado", "Added", criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value).Split('.')[0]), // AddedTimeFormat
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Agregado", "Added", criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value).Split('.')[1]), // AddedTimeFormatEnglish
                    _timeFormatService.GetDateTimeFormatMonthToltip(criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // AddedTimeFormatToltip,
                    criteria.Position.Name,
                    criteria.Position.NameEnglish,
                    criteria.CollaboratorCriteriaAnswers[0].Comments,
                    criteriaObjetiveResultTemp,
                    criteriaSubjetiveResultTemp,
                    criteria.CollaboratorCriteriaAnswers[0].ImprovementPlans.Count > 0, // RequireImprovementPlan
                    criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value // CreaationDate    
                );
                if (!query.IsInHistory)
                {
                    if (criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value > oldPosition.CriteriasEditionDate.Value)
                    {
                        responseNoHistory.Add(temp);

                    }
                }
                else
                {
                    if (criteria.CollaboratorCriteriaAnswers[0].IsInHistorical)
                    {
                        response.Add(temp);

                    }
                }

            }
        }
        return query.IsInHistory ? [.. response.OrderByDescending(x => x.CreaationDate)] : responseNoHistory.OrderByDescending(x => x.CreaationDate).ToList();
    }
}