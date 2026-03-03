using HR_Platform.Domain.ImprovementPlanResponseFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.ImprovementPlanResponses;

public sealed class ImprovementPlanResponse : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private ImprovementPlanResponse()
    {
    }

    public ImprovementPlanResponse(ImprovementPlanResponseId id, ImprovementPlanTaskId improvementPlanTaskId, string taskResponse, 
        bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        ImprovementPlanTaskId = improvementPlanTaskId;

        TaskResponse = taskResponse;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public ImprovementPlanResponseId Id { get; set; }

    public ImprovementPlanTaskId ImprovementPlanTaskId { get; set; }
    public ImprovementPlanTask ImprovementPlanTask { get; set; }

    public string TaskResponse { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<ImprovementPlanResponseFile> ImprovementPlanResponseFiles { get; set; }

}


