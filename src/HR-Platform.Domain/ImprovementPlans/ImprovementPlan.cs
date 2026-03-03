using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.ImprovementPlans;

public sealed class ImprovementPlan : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private ImprovementPlan()
    {
    }

    public ImprovementPlan(ImprovementPlanId id, CollaboratorCriteriaAnswerId collaboratorCriteriaAnswerId, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorCriteriaAnswerId = collaboratorCriteriaAnswerId;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public ImprovementPlanId Id { get; set; }

    public CollaboratorCriteriaAnswerId CollaboratorCriteriaAnswerId { get; set; }
    public CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<ImprovementPlanTask> ImprovementPlanTasks { get; set; }

}

