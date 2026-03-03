using ErrorOr;
using HR_Platform.Domain.DefaultEvaluationCriterias;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.UpdateCriteriaByPosition;

internal sealed class UpdateCriteriaByPositionCommandHandler(
    IPositionRepository positionRepository,
    IEvaluationCriteriaRepository evaluationCriteriaRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<UpdateCriteriaByPositionCommand, ErrorOr<bool>>
{
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository = evaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateCriteriaByPositionCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Minutes.CreationDate", "CreationDate is not valid");

        if (await positionRepository.GetByIdAsync(new PositionId(command.PositionId)) is not Position oldPosition)
            return Error.NotFound("EvaluationCriteria.NotFound", "The Position related with the provide Name was not found.");

        if (await _evaluationCriteriaRepository.GetByPositionIdAndEvaluationCriteriaTypeIdAsync(oldPosition.Id, new EvaluationCriteriaTypeId(command.CriteriaIdentifier)) is not List<EvaluationCriteria> objectiveCriterias)
            return Error.NotFound("EvaluationCriteria.NotFound", "The Evaluation Criterias related with the provide Id was not found.");
        
        //Validation For add new 
        if (command.CriteriasList.Count > 10)
            return Error.Validation("EvaluationCriteria.AddNew", "There can only be a maximum of 10 criteria per type");

        int criteriasSum = command.CriteriasList.Sum(x => x.Percentage);

        if (criteriasSum != 100)
            return Error.Validation("EvaluationCriteria.Validation", "Incomplete percentages, must be at 100%");

        var idsToUpdate = command.CriteriasList.Select(x => x.Id).ToList();
        var originalList = objectiveCriterias.Where(x => idsToUpdate.Contains(x.Id.Value)).ToList();

        var updatesDict = command.CriteriasList.ToDictionary(x => x.Id);

        List<EvaluationCriteria> evaluationCriteriaList = [];

        //Agregar cualquier guid en el id para los nuevos 
        foreach (var item in originalList)
        {
            if (updatesDict.TryGetValue(item.Id.Value, out var updatedItem))
            {
                item.Name = updatedItem.Name;
                item.NameEnglish = updatedItem.Name != item.Name ? updatedItem.Name : item.NameEnglish;
                item.Description = updatedItem.Description;
                item.DescriptionEnglish = updatedItem.Description != item.Description ? updatedItem.Description : item.DescriptionEnglish;
                item.Percentage = updatedItem.Percentage;
                item.EditionDate = editionDate;
            }
            else
            {
                EvaluationCriteria evaluationCriteria = new
                (
                    new EvaluationCriteriaId(Guid.NewGuid()),
                    new EvaluationCriteriaTypeId(command.CriteriaIdentifier),
                    new PositionId(command.PositionId),
                    updatedItem is not null ? updatedItem.Name : string.Empty,
                    updatedItem is not null ? updatedItem.Name : string.Empty, //NameEnglish,
                    updatedItem is not null ? updatedItem.Description : string.Empty,
                    updatedItem is not null ? updatedItem.Description : string.Empty,//DescriptionEnglish,
                    updatedItem is not null ? updatedItem.Percentage : 0,
                    true, // IsEditable
                    true, // IsDeleteable
                    editionDate,
                    editionDate
                );
                evaluationCriteriaList.Add( evaluationCriteria );
            }
        }
        if (updatesDict.Count > 0)
        {
            oldPosition.CriteriasEditionDate = editionDate;
            _positionRepository.Update(oldPosition);
        }

        _evaluationCriteriaRepository.UpdateRange(objectiveCriterias);

        if (evaluationCriteriaList.Count > 0)
        {
            _evaluationCriteriaRepository.AddRangeEvaluationCriterias(evaluationCriteriaList);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}