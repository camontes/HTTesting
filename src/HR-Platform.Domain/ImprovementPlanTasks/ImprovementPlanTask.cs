using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.ImprovementPlanTasks;

public sealed class ImprovementPlanTask : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private ImprovementPlanTask()
    {
    }

    public ImprovementPlanTask(ImprovementPlanTaskId id, ImprovementPlanId improvementPlanId, string taskDescription, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        ImprovementPlanId = improvementPlanId;

        TaskDescription = taskDescription;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public ImprovementPlanTaskId Id { get; set; }

    public ImprovementPlanId ImprovementPlanId { get; set; }
    public ImprovementPlan ImprovementPlan { get; set; }

    public string TaskDescription { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<ImprovementPlanResponse> ImprovementPlanResponse { get; set; }
    public List<ImprovementPlanTaskFile> ImprovementPlanTaskFiles { get; set; }

}

