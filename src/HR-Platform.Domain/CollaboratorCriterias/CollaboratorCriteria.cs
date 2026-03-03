using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorCriterias;

public sealed class CollaboratorCriteria : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorCriteria()
    {
    }

    public CollaboratorCriteria(CollaboratorCriteriaId id, CollaboratorId collaboratorEvaluatedId, CollaboratorId evaluatorId, PositionId positionId, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorEvaluatedId = collaboratorEvaluatedId;
        EvaluatorId = evaluatorId;
        PositionId = positionId;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorCriteriaId Id { get; set; }

    public CollaboratorId CollaboratorEvaluatedId { get; set; }
    public Collaborator CollaboratorEvaluated { get; set; }

    public CollaboratorId EvaluatorId { get; set; }
    public Collaborator Evaluator { get; set; }

    public PositionId PositionId { get; set; }
    public Position Position { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswers { get; set; }

}

