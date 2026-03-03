using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.CreateEvalutionByEvaluator;

internal sealed class CreateEvalutionByEvaluatorCommandHandler(
    ICollaboratorCriteriaAnswerRepository collaboratorCriteriaAnswerRepository,
    IReferenceGenerator referenceGenerator,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateEvalutionByEvaluatorCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorCriteriaAnswerRepository _collaboratorCriteriaAnswerRepository = collaboratorCriteriaAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaAnswerRepository));
    private readonly IReferenceGenerator _referenceGenerator = referenceGenerator ?? throw new ArgumentNullException(nameof(referenceGenerator));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateEvalutionByEvaluatorCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("EvalutionByEvaluator.CreationDate", "CreationDate is not valid");

        List<CollaboratorCriteriaAnswer> requestCollaboratorCriteriaAnswers = [];

        string radicate = _referenceGenerator.GenerateReference("EVA");

        foreach (CriteriaAnswer answer in command.CriteriaAnswerList)
        {
            CollaboratorCriteriaAnswer temp = new
            (
                new CollaboratorCriteriaAnswerId(Guid.NewGuid()),
                new EvaluationCriteriaTypeId(answer.EvaluationCriteriaTypeId),
                command.ObjectiveCriteriaValue,
                command.SubjectiveCriteriaValue,
                new CollaboratorCriteriaId(command.CollaboratorCriteriaId),
                answer.CriteriaName,
                answer.CriteriaNameEnglish,
                answer.CriteriaPercentage,
                answer.CriteriaScoreDescription,
                answer.CriteriaScoreDescriptionEnglish,
                answer.CriteriaSingleScorePercentage,
                answer.CriteriaScoreIndexAnswer,
                radicate, //Reference Number
                command.PositionName,
                command.PositionNameEnglish,
                command.Comments,
                false, // isInHistorical
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                creationDate //EditionDate
            );
            requestCollaboratorCriteriaAnswers.Add(temp);
        }

        _collaboratorCriteriaAnswerRepository.AddRange(requestCollaboratorCriteriaAnswers);

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