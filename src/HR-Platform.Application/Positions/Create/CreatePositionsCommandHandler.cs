using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DefaultEvaluationCriterias;
using HR_Platform.Domain.DefaultEvaluationCriteriaScores;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Positions.Create;

internal sealed class CreatePositionsCommandHandler(IDefaultEvaluationCriteriaRepository defaultEvaluationCriteriaRepository,
                                     IDefaultEvaluationCriteriaScoreRepository defaultEvaluationCriteriaScoreRepository,
                                     IEvaluationCriteriaRepository evaluationCriteriaRepository,
                                     IEvaluationCriteriaScoreRepository evaluationCriteriaScoreRepository,
                                     IPositionRepository positionRepository,
                                     IUnitOfWork unitOfWork) : IRequestHandler<CreatePositionsCommand, ErrorOr<Guid>>
{
    private readonly IDefaultEvaluationCriteriaRepository _defaultEvaluationCriteriaRepository = defaultEvaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(defaultEvaluationCriteriaRepository));
    private readonly IDefaultEvaluationCriteriaScoreRepository _defaultEvaluationCriteriaScoreRepository = defaultEvaluationCriteriaScoreRepository ?? throw new ArgumentNullException(nameof(defaultEvaluationCriteriaScoreRepository));
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository = evaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaRepository));
    private readonly IEvaluationCriteriaScoreRepository _evaluationCriteriaScoreRepository = evaluationCriteriaScoreRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaScoreRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<Guid>> Handle(CreatePositionsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        string editionDateString = creationDateString;
        string criteriasEditionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Pensions.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Pensions.EditionDate", "EditionDate is not valid");

        if (TimeDate.Create(criteriasEditionDateString) is not TimeDate criteriasEditionDate)
            return Error.Validation("Pensions.CriteriasEditionDate", "CriteriasEditionDate is not valid");

        if (await _defaultEvaluationCriteriaRepository.GetAll() is not List<DefaultEvaluationCriteria> defaultEvaluationCriterias)
            return Error.NotFound("DefaultEvaluationCriteria.NotFound", "The DefaultEvaluationCriterias was not found.");

        Position position = new
        (
            new PositionId(Guid.NewGuid()),

            new CompanyId(Guid.Parse(command.CompanyId)),

            command.Name,
            command.NameEnglish,

            !string.IsNullOrEmpty(command.Description) ? command.Description : string.Empty,
            !string.IsNullOrEmpty(command.DescriptionEnglish) ? command.DescriptionEnglish : string.Empty,

            !string.IsNullOrEmpty(command.PositionURL) ? command.PositionURL : string.Empty,
            !string.IsNullOrEmpty(command.PositionFileName) ? command.PositionFileName : string.Empty,

            70, // Objective Criteria
            30, // Subjective Criteria,

            command.IsEditable,
            command.IsDeleteable,

            creationDate,
            editionDate,
            criteriasEditionDate
        );

        _positionRepository.Add(position);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        /**/

        try
        {
            if (position is not null && defaultEvaluationCriterias is not null && defaultEvaluationCriterias.Count > 0)
            {
                foreach (DefaultEvaluationCriteria defaultEvaluationCriteria in defaultEvaluationCriterias)
                {
                    if (await _defaultEvaluationCriteriaScoreRepository.GetByDefaultEvaluationCriteriaIdAsync(defaultEvaluationCriteria.Id.Value) is not List<DefaultEvaluationCriteriaScore> defaultEvaluationCriteriaScores)
                        return Error.NotFound("DefaultEvaluationCriteriaScore.NotFound", "The DefaultEvaluationCriteriaScores was not found.");

                    EvaluationCriteria evaluationCriteria = new
                        (
                            new EvaluationCriteriaId(Guid.NewGuid()),

                            new EvaluationCriteriaTypeId(defaultEvaluationCriteria.EvaluationCriteriaTypeId),

                            position.Id,

                            defaultEvaluationCriteria.Name,
                            defaultEvaluationCriteria.NameEnglish,

                            defaultEvaluationCriteria.Description,
                            defaultEvaluationCriteria.DescriptionEnglish,

                            defaultEvaluationCriteria.Percentage,

                            true, // IsEditable
                            false, // IsDeleteable

                            creationDate,
                            editionDate
                        );

                    _evaluationCriteriaRepository.Add(evaluationCriteria);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    if (defaultEvaluationCriteriaScores is not null && defaultEvaluationCriteriaScores.Count > 0)
                    {
                        int index = 1;

                        foreach (DefaultEvaluationCriteriaScore defaultEvaluationCriteriaScore in defaultEvaluationCriteriaScores)
                        {
                            EvaluationCriteriaScore evaluationCriteriaScore = new
                            (
                                new EvaluationCriteriaScoreId(Guid.NewGuid()),

                                evaluationCriteria.Id,

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
            }
        }
        catch(Exception ex)
        {

        }

        /**/

        return position is not null ? position.Id.Value : Guid.NewGuid();
    }
}