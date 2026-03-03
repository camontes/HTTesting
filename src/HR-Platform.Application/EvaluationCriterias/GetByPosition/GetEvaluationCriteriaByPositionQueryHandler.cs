using ErrorOr;
using HR_Platform.Application.EvaluationCriterias.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.EvaluationCriterias.GetByPosition;

internal sealed class GetEvaluationCriteriaByPositionQueryHandler(
    IEvaluationCriteriaRepository evaluationCriteriaRepository,
    IPositionRepository positionRepository,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetEvaluationCriteriaByPositionQuery, ErrorOr<EvaluationCriteriaResponse>>
{
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository = evaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    #pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.

    public async Task<ErrorOr<EvaluationCriteriaResponse>> Handle(GetEvaluationCriteriaByPositionQuery query, CancellationToken cancellationToken)
    {
        if (await _positionRepository.GetByIdAsync(new PositionId(query.PositionId)) is not Position oldPosition)
            return Error.NotFound("EvaluationCriteria.NotFound", "The Position related with the provide Name was not found.");

        string textDateDifference = string.Empty;
        string textDateDifferenceEnglish = string.Empty;

        bool IsUpdated = oldPosition.CreationDate.Value != oldPosition.CriteriasEditionDate.Value;

        if (IsUpdated)
        {
            textDateDifference = "Actualizado";
            textDateDifferenceEnglish = "Updated";
        }

        if (await _evaluationCriteriaRepository.GetByPositionIdAndEvaluationCriteriaTypeIdAsync(new PositionId(query.PositionId), new EvaluationCriteriaTypeId(1)) is not List<EvaluationCriteria> objectiveCriterias)
            return Error.NotFound("EvaluationCriteria.NotFound", "The Evaluation Criterias related with the provide Id was not found.");

        if (await _evaluationCriteriaRepository.GetByPositionIdAndEvaluationCriteriaTypeIdAsync(new PositionId(query.PositionId), new EvaluationCriteriaTypeId(2)) is not List<EvaluationCriteria> subjectiveCriterias)
            return Error.NotFound("EvaluationCriteria.NotFound", "The Evaluation Criterias related with the provide Id was not found.");

        List<CriteriasResponse> objectiveCriteriasResponses = [];
        List<CriteriasResponse> subjectiveCriteriasResponses = [];

        if (objectiveCriterias is not null && objectiveCriterias.Count > 0)
        {
            foreach (EvaluationCriteria objectiveCriteria in objectiveCriterias)
            {
                List<CriteriasScoreResponse> criteriasScoreResponses = [];

                if (objectiveCriteria is not null && objectiveCriteria.EvaluationCriteriaScores is not null && objectiveCriteria.EvaluationCriteriaScores.Count > 0)
                {
                    foreach (EvaluationCriteriaScore evaluationCriteriaScore in objectiveCriteria.EvaluationCriteriaScores)
                    {
                        CriteriasScoreResponse criteriasScoreResponse = new
                            (
                                evaluationCriteriaScore.Id.Value,

                                evaluationCriteriaScore.EvaluationCriteriaId.Value,

                                evaluationCriteriaScore.Description,
                                evaluationCriteriaScore.DescriptionEnglish,

                                evaluationCriteriaScore.LowerScore,
                                evaluationCriteriaScore.UpperScore,
                                evaluationCriteriaScore.IndexScoreAnswer
                            );

                        criteriasScoreResponses.Add(criteriasScoreResponse);
                    }
                }

                CriteriasResponse objectiveCriteriasResponse = new
                    (
                        objectiveCriteria.Id.Value,

                        objectiveCriteria.EvaluationCriteriaTypeId.Value,

                        objectiveCriteria.Name,
                        objectiveCriteria.NameEnglish,

                        objectiveCriteria.Description,
                        objectiveCriteria.DescriptionEnglish,

                        objectiveCriteria.Percentage,

                        objectiveCriteria.IsEditable,
                        objectiveCriteria.IsDeleteable,

                        [.. criteriasScoreResponses.OrderBy(x => x.UpperScore)]
                    );

                objectiveCriteriasResponses.Add(objectiveCriteriasResponse);
            }
        }

        if (subjectiveCriterias is not null && subjectiveCriterias.Count > 0)
        {
            foreach (EvaluationCriteria subjectiveCriteria in subjectiveCriterias)
            {
                List<CriteriasScoreResponse> criteriasScoreResponses = [];

                if (subjectiveCriteria is not null && subjectiveCriteria.EvaluationCriteriaScores is not null && subjectiveCriteria.EvaluationCriteriaScores.Count > 0)
                {
                    foreach (EvaluationCriteriaScore evaluationCriteriaScore in subjectiveCriteria.EvaluationCriteriaScores)
                    {
                        CriteriasScoreResponse criteriasScoreResponse = new
                            (
                                evaluationCriteriaScore.Id.Value,

                                evaluationCriteriaScore.EvaluationCriteriaId.Value,

                                evaluationCriteriaScore.Description,
                                evaluationCriteriaScore.DescriptionEnglish,

                                evaluationCriteriaScore.LowerScore,
                                evaluationCriteriaScore.UpperScore,
                                evaluationCriteriaScore.IndexScoreAnswer

                            );

                        criteriasScoreResponses.Add(criteriasScoreResponse);
                    }
                }

                CriteriasResponse subjectiveCriteriasResponse = new
                    (
                        subjectiveCriteria.Id.Value,

                        subjectiveCriteria.EvaluationCriteriaTypeId.Value,

                        subjectiveCriteria.Name,
                        subjectiveCriteria.NameEnglish,

                        subjectiveCriteria.Description,
                        subjectiveCriteria.DescriptionEnglish,

                        subjectiveCriteria.Percentage,

                        subjectiveCriteria.IsEditable,
                        subjectiveCriteria.IsDeleteable,

                        [.. criteriasScoreResponses.OrderBy(x => x.UpperScore)]
                    );

                subjectiveCriteriasResponses.Add(subjectiveCriteriasResponse);
            }
        }

        EvaluationCriteriaResponse response = new
        (
            oldPosition.Id.Value,

            oldPosition.Name,
            oldPosition.NameEnglish,

            oldPosition.SubjectiveCriteria,
            oldPosition.ObjectiveCriteria,

            objectiveCriteriasResponses,
            subjectiveCriteriasResponses,

            IsUpdated ? string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction(textDateDifference, textDateDifferenceEnglish, oldPosition.CriteriasEditionDate.Value).Split('.')[0]) : string.Empty,
            IsUpdated ? string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction(textDateDifference, textDateDifferenceEnglish, oldPosition.CriteriasEditionDate.Value).Split('.')[1]) : string.Empty,
            IsUpdated ? _timeFormatService.GetDateTimeFormatMonthToltip(oldPosition.CriteriasEditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty
        );

        return response;

        #pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
    }
}