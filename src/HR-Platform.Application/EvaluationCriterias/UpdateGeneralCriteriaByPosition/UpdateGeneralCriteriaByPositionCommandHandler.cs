using ErrorOr;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.UpdateGeneralCriteriaByPosition;

internal sealed class UpdateGeneralCriteriaByPositionCommandHandler(
    IPositionRepository positionRepository,
    IEvaluationCriteriaRepository evaluationCriteriaRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<UpdateGeneralCriteriaByPositionCommand, ErrorOr<bool>>
{
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository = evaluationCriteriaRepository ?? throw new ArgumentNullException(nameof(evaluationCriteriaRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateGeneralCriteriaByPositionCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Minutes.CreationDate", "CreationDate is not valid");

        if (await _positionRepository.GetByIdAsync(new PositionId(command.PositionId)) is not Position oldPosition)
            return Error.NotFound("EvaluationCriteria.NotFound", "The Position related with the provide Name was not found.");

        if (command.ObjectiveCriteria + command.SubjectiveCriteria != 100)
            return Error.Validation("EvaluationCriteria.Validation", "Incomplete percentages, must be at 100%");

        oldPosition.SubjectiveCriteria = command.SubjectiveCriteria;
        oldPosition.ObjectiveCriteria = command.ObjectiveCriteria;
        oldPosition.CriteriasEditionDate = editionDate;

        _positionRepository.Update(oldPosition);

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