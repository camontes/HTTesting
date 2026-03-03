using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorGeneralInductions;

public sealed class CollaboratorGeneralInduction : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorGeneralInduction()
    {
    }

    public CollaboratorGeneralInduction(CollaboratorGeneralInductionId id, CollaboratorId collaboratorId, InductionId inductionId, bool hasInductionBeenDeletedWhenHasCompleted, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;
        InductionId = inductionId;

        HasInductionBeenDeletedWhenHasCompleted = hasInductionBeenDeletedWhenHasCompleted;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorGeneralInductionId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public InductionId InductionId { get; set; }
    public Induction Induction { get; set; }

    public bool HasInductionBeenDeletedWhenHasCompleted { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

