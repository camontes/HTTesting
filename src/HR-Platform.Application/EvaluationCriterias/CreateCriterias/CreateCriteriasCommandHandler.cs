using ErrorOr;
using HR_Platform.Domain.DefaultEvaluationCriteriaScores;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.CreateCriterias;

internal sealed class CreateDefaultCriteriasByPositionCommandHandler(
    IDefaultEvaluationCriteriaScoreRepository defaultEvaluationCriteriaScoreRepository,
    IEvaluationCriteriaRepository evaluationCriteriaRepository,
    IEvaluationCriteriaScoreRepository evaluationCriteriaScoreRepository,
    IPositionRepository positionRepository,

    IUnitOfWork unitOfWork

    ) : IRequestHandler<CreateCriteriasCommand, ErrorOr<bool>>
{
    private readonly IDefaultEvaluationCriteriaScoreRepository _defaultEvaluationCriteriaScoreRepository = defaultEvaluationCriteriaScoreRepository ?? throw new ArgumentNullException(nameof(defaultEvaluationCriteriaScoreRepository));
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository = evaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaRepository));
    private readonly IEvaluationCriteriaScoreRepository _evaluationCriteriaScoreRepository = evaluationCriteriaScoreRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaScoreRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateCriteriasCommand command, CancellationToken cancellationToken)
    {
        try
        {
            DateTime colombianHour = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
            string creationDateString = colombianHour.ToString("MM/dd/yyyy HH:mm:ss");

            string editionDateString = creationDateString;

            if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
                return Error.Validation("Collaborators.CreationDate", "CreationDate is not valid");

            if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
                return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

            if (await _positionRepository.GetByIdWithoutChildrenAsync(new PositionId(command.PositionId)) is not Position oldPosition)
                return Error.NotFound("EvaluationCriteria.NotFound", "The Position related with the provide Name was not found.");

            if (await _defaultEvaluationCriteriaScoreRepository.GetByDefaultEvaluationCriteriaIdAsync(8) is not List<DefaultEvaluationCriteriaScore> defaultEvaluationCriteriaScores)
                return Error.NotFound("DefaultEvaluationCriteria.NotFound", "The DefaultEvaluationCriterias was not found.");

            List<EvaluationCriteria>? oldCriterias = await _evaluationCriteriaRepository.GetByPositionIdAsync(oldPosition.Id);

            List<BaseEvaluationCriterias> criteriasToAddOrUpdate = [];
            List<EvaluationCriteria> criteriasToDelete = [];

            criteriasToAddOrUpdate = command.CriteriasList;

            if (oldCriterias is not null)
            {
                criteriasToDelete =
                [
                    ..
                    oldCriterias
                    .Where
                    (
                        criteriaFromDB
                        =>
                        command.CriteriasList is not null
                        &&
                        !command.CriteriasList
                        .Any
                        (
                            criteriaFromClient
                            =>
                            !string.IsNullOrEmpty(criteriaFromClient.Id)
                            &&
                            new EvaluationCriteriaId(Guid.Parse(criteriaFromClient.Id)) == criteriaFromDB.Id
                        )
                    )
                ];
            }

            if (oldPosition is not null && criteriasToAddOrUpdate is not null && criteriasToAddOrUpdate.Count > 0)
            {
                foreach (BaseEvaluationCriterias evaluationCriteria in criteriasToAddOrUpdate)
                {
                    if (evaluationCriteria is not null && string.IsNullOrEmpty(evaluationCriteria.Id.ToString()))
                    {
                        EvaluationCriteria newEvaluationCriteria = new
                        (
                            new EvaluationCriteriaId(Guid.NewGuid()),

                            new EvaluationCriteriaTypeId(evaluationCriteria.EvaluationCriteriaTypeId),

                            new PositionId(command.PositionId),

                            evaluationCriteria.Name,
                            evaluationCriteria.Name,

                            evaluationCriteria.Description,
                            evaluationCriteria.Description,

                            evaluationCriteria.Percentage,

                            true, // IsEditable
                            true, // IsDeleteable

                            creationDate,
                            editionDate
                        );

                        _evaluationCriteriaRepository.Add(newEvaluationCriteria);

                        await _unitOfWork.SaveChangesAsync(cancellationToken);

                        oldPosition.CriteriasEditionDate = editionDate;

                        _positionRepository.Update(oldPosition);

                        await _unitOfWork.SaveChangesAsync(cancellationToken);

                        if (defaultEvaluationCriteriaScores is not null && defaultEvaluationCriteriaScores.Count > 0)
                        {
                            int index = 1;

                            foreach (DefaultEvaluationCriteriaScore defaultEvaluationCriteriaScore in defaultEvaluationCriteriaScores)
                            {
                                EvaluationCriteriaScore evaluationCriteriaScore = new
                                (
                                    new EvaluationCriteriaScoreId(Guid.NewGuid()),

                                    newEvaluationCriteria.Id,

                                    defaultEvaluationCriteriaScore.Description,
                                    defaultEvaluationCriteriaScore.DescriptionEnglish,

                                    defaultEvaluationCriteriaScore.LowerScore,
                                    defaultEvaluationCriteriaScore.UpperScore,

                                    index,

                                    true, // IsEditable
                                    true, // IsDeleteable

                                    creationDate,
                                    editionDate
                                );

                                _evaluationCriteriaScoreRepository.Add(evaluationCriteriaScore);

                                await _unitOfWork.SaveChangesAsync(cancellationToken);

                                index++;
                            }
                        }
                    }
                    else if (evaluationCriteria is not null && !string.IsNullOrEmpty(evaluationCriteria.Id.ToString()))
                    {
                        if (await _evaluationCriteriaRepository.GetByIdAsync(new EvaluationCriteriaId(Guid.Parse(evaluationCriteria.Id))) is not EvaluationCriteria oldEvaluationCriteria)
                            return Error.NotFound("DefaultEvaluationCriteria.NotFound", "The DefaultEvaluationCriterias was not found.");

                        bool isChangeName = false;

                        if (oldEvaluationCriteria.Name != evaluationCriteria.Name)
                            isChangeName = true;

                        bool isChangeDescription = false;

                        if (oldEvaluationCriteria.Description != evaluationCriteria.Description)
                            isChangeDescription = true;

                        EvaluationCriteria updatedEvaluationCriteria = new
                        (
                            oldEvaluationCriteria.Id,

                            oldEvaluationCriteria.EvaluationCriteriaTypeId,

                            oldEvaluationCriteria.PositionId,

                            isChangeName ? evaluationCriteria.Name : oldEvaluationCriteria.Name,
                            isChangeName ? evaluationCriteria.Name : oldEvaluationCriteria.NameEnglish,

                            isChangeDescription ? evaluationCriteria.Description : oldEvaluationCriteria.Description,
                            isChangeDescription ? evaluationCriteria.Description : oldEvaluationCriteria.DescriptionEnglish,

                            evaluationCriteria.Percentage,

                            oldEvaluationCriteria.IsEditable,
                            oldEvaluationCriteria.IsDeleteable,

                            oldEvaluationCriteria.CreationDate,
                            editionDate
                        );

                        _evaluationCriteriaRepository.Update(updatedEvaluationCriteria);

                        await _unitOfWork.SaveChangesAsync(cancellationToken);

                        oldPosition.CriteriasEditionDate = editionDate;

                        _positionRepository.Update(oldPosition);

                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
                }
            }

            if (criteriasToDelete is not null && criteriasToDelete.Count > 0)
            {
                _evaluationCriteriaRepository.DeleteRange(criteriasToDelete);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}