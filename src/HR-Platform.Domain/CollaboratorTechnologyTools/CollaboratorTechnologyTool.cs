using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultKnowledgeLevels;
using HR_Platform.Domain.DefaultTechnologyNames;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorTechnologyTools;

public sealed class CollaboratorTechnologyTool : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorTechnologyTool()
    {
    }

    public CollaboratorTechnologyTool(CollaboratorTechnologyToolId id, CollaboratorId collaboratorId, DefaultTechnologyNameId? defaultTechnologyNameId,
        DefaultKnowledgeLevelId? defaultKnowledgeLevelId, string otherTechnologyName, string otherTechnologyNameEnglish,
        string? otherKnowledgeLevelName, string? otherKnowledgeLevelNameEnglish, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        DefaultTechnologyNameId = defaultTechnologyNameId;
        DefaultKnowledgeLevelId = defaultKnowledgeLevelId;

        OtherTechnologyName = otherTechnologyName;
        OtherTechnologyNameEnglish = otherTechnologyNameEnglish;

        OtherKnowledgeLevelName = otherKnowledgeLevelName;
        OtherKnowledgeLevelNameEnglish = otherKnowledgeLevelNameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorTechnologyToolId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public DefaultTechnologyNameId? DefaultTechnologyNameId { get; set; }
    public DefaultTechnologyName? DefaultTechnologyName { get; set; }

    public DefaultKnowledgeLevelId? DefaultKnowledgeLevelId { get; set; }
    public DefaultKnowledgeLevel? DefaultKnowledgeLevel { get; set; }

    public string? OtherTechnologyName { get; set; } = string.Empty;
    public string? OtherTechnologyNameEnglish { get; set; } = string.Empty;

    public string? OtherKnowledgeLevelName { get; set; } = string.Empty;
    public string? OtherKnowledgeLevelNameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

