using ErrorOr;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.CreateCollaboratorToEvaluator;

internal sealed class CreateCollaboratorToEvaluatorCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorCriteriaRepository collaboratorCriteriaRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCollaboratorToEvaluatorCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorCriteriaRepository _collaboratorCriteriaRepository = collaboratorCriteriaRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateCollaboratorToEvaluatorCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("CollaboratorToEvaluators.CreationDate", "CreationDate is not valid");

        List<Collaborator> collaboratorsByPosition = await _collaboratorRepository.GetByPositionId(new PositionId(command.PositionId));

        List<CollaboratorCriteria> requestCollaboratorCriteria = [];


        if (collaboratorsByPosition is not null && collaboratorsByPosition.Count > 0)
        {
            foreach (Collaborator collaborator in collaboratorsByPosition)
            {
                bool IsExsisting = await _collaboratorCriteriaRepository.IsExistingByCollaboratorIdAndEvaluatorIdAsync(collaborator.Id, new CollaboratorId(command.EvaluatorId), new PositionId(command.PositionId));
                if (IsExsisting)
                    return Error.Validation("CollaboratorToEvaluators.IsExsisting", "One contributor has already registered with the evaluator");

                CollaboratorCriteria relation = new
                (
                    new CollaboratorCriteriaId(Guid.NewGuid()),
                    collaborator.Id, // CollaboratorEvaluatedId
                    new CollaboratorId(command.EvaluatorId), // EvaluatorId
                    new PositionId(command.PositionId),
                    true, //IsEditable
                    true, //IsDeletable
                    creationDate,
                    creationDate
                );

                // If the collaborator is not the evaluator
                if (collaborator.Id.Value != command.EvaluatorId)
                {
                    if (command.IsForAll)
                    {
                        requestCollaboratorCriteria.Add(relation);
                    }
                    else
                    {
                        if (command.CollaboratorIds is not null && command.CollaboratorIds.Any(x => collaborator.Id.Value == x))
                        {
                            requestCollaboratorCriteria.Add(relation);
                        }
                    }
                }
            }
        }

        _collaboratorCriteriaRepository.AddRange(requestCollaboratorCriteria);

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