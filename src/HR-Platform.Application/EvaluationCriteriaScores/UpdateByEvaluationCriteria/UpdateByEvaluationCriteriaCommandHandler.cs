using ErrorOr;
using HR_Platform.Application.EvaluationCriteriaScores.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.EvaluationCriteriaScores.UpdateByEvaluationCriteria;

internal sealed class UpdateByEvaluationCriteriaCommandHandler
(
    IEvaluationCriteriaRepository evaluationCriteriaRepository,
    IEvaluationCriteriaScoreRepository evaluationCriteriaScoreRepository,
    IPositionRepository positionRepository,

    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference,

    IUnitOfWork unitOfWork

) : IRequestHandler<UpdateCriteriaScoresByCriteriaCommand, ErrorOr<bool>>
{
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository = evaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaRepository));
    private readonly IEvaluationCriteriaScoreRepository _evaluationCriteriaScoreRepository = evaluationCriteriaScoreRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaScoreRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));

    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateCriteriaScoresByCriteriaCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Minutes.CreationDate", "CreationDate is not valid");

        if (await _positionRepository.GetByIdWithoutChildrenAsync(new PositionId(command.PositionId)) is not Position oldPosition)
            return Error.NotFound("Position.NotFound", "The Position related with the provide Name was not found.");

        if (await _evaluationCriteriaRepository.GetByPositionIdWithChildrenAsync(new PositionId(command.PositionId)) is not List<EvaluationCriteria> evaluationCriterias)
            return Error.NotFound("EvaluationCriteria.NotFound", "The EvaluationCriteria related with the provide Name was not found.");

        string textDateDifference = string.Empty;
        string textDateDifferenceEnglish = string.Empty;

        bool IsUpdated = oldPosition.CreationDate.Value != oldPosition.CriteriasEditionDate.Value;

        if (IsUpdated)
        {
            textDateDifference = "Actualizado";
            textDateDifferenceEnglish = "Updated";
        }

        EvaluationCriteriaScoreResponse evaluationCriteriaScoreResponse = new
        (
            oldPosition.Id.Value,

            oldPosition.Name,
            oldPosition.NameEnglish,

            oldPosition.SubjectiveCriteria,
            oldPosition.ObjectiveCriteria,

            [],

            [],

            IsUpdated ? string.Join(" ", _calculateTimeDifference
                .CalculateTimeDifferenceFunction(textDateDifference, textDateDifferenceEnglish, oldPosition.CriteriasEditionDate.Value).Split('.')[0]) : string.Empty,
            IsUpdated ? string.Join(" ", _calculateTimeDifference
                .CalculateTimeDifferenceFunction(textDateDifference, textDateDifferenceEnglish, oldPosition.CriteriasEditionDate.Value).Split('.')[1]) : string.Empty,
            IsUpdated ? _timeFormatService.GetDateTimeFormatMonthToltip(oldPosition.CriteriasEditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty
        );

        if (oldPosition is not null && evaluationCriterias is not null && evaluationCriterias.Count > 0)
        {
            foreach(EvaluationCriteria evaluationCriteria in evaluationCriterias)
            {
                CriteriasResponse criteria = new
                (
                    evaluationCriteria.Id.Value,

                    evaluationCriteria.EvaluationCriteriaTypeId.Value,

                    evaluationCriteria.Name,
                    evaluationCriteria.NameEnglish,

                    evaluationCriteria.Description,
                    evaluationCriteria.DescriptionEnglish,

                    evaluationCriteria.Percentage,

                    []
                );

                if (evaluationCriteria is not null && evaluationCriteria.EvaluationCriteriaScores is not null && evaluationCriteria.EvaluationCriteriaScores.Count > 0)
                {
                    foreach(EvaluationCriteriaScore evaluationCriteriaScore in evaluationCriteria.EvaluationCriteriaScores)
                    {
                        EvaluationCriteriasScoreCommand? evaluationCriteriaScoreCommand
                            = command.EvaluationCriteriaScores.FirstOrDefault(ec => ec.EvaluationCriteriaScoreId == evaluationCriteriaScore.Id.Value);

                        if (evaluationCriteriaScoreCommand is not null)
                        {
                            evaluationCriteriaScore.Description = evaluationCriteriaScoreCommand.Description;
                            evaluationCriteriaScore.DescriptionEnglish = evaluationCriteriaScoreCommand.DescriptionEnglish;

                            evaluationCriteriaScore.LowerScore = evaluationCriteriaScoreCommand.LowerScore;
                            evaluationCriteriaScore.UpperScore = evaluationCriteriaScoreCommand.UpperScore;

                            evaluationCriteriaScore.EditionDate = editionDate;

                            _evaluationCriteriaScoreRepository.Update(evaluationCriteriaScore);

                            await _unitOfWork.SaveChangesAsync(cancellationToken);

                            CriteriasScoreResponse score = new
                            (
                                evaluationCriteriaScore.Id.Value,

                                evaluationCriteriaScore.EvaluationCriteriaId.Value,

                                evaluationCriteriaScore.Description,
                                evaluationCriteriaScore.DescriptionEnglish,

                                evaluationCriteriaScore.LowerScore,
                                evaluationCriteriaScore.UpperScore
                            );

                            criteria.CriteriasScore.Add(score);
                        }
                    }

                    if (evaluationCriteria.EvaluationCriteriaTypeId.Value == 1)
                        evaluationCriteriaScoreResponse.ObjectiveCriterias.Add(criteria);

                    if (evaluationCriteria.EvaluationCriteriaTypeId.Value == 2)
                        evaluationCriteriaScoreResponse.SubjectiveCriterias.Add(criteria);
                }
            }

            try
            {
                oldPosition.CriteriasEditionDate = editionDate;

                _positionRepository.Update(oldPosition);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return false;
    }
}