using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.FamilyCompositions;

public sealed class FamilyComposition : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private FamilyComposition()
    {
    }

    public FamilyComposition(FamilyCompositionId id, CollaboratorId collaboratorId, string name, string nameEnglish, bool isEditable, bool isDeleteable, 
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        Name = name;
        NameEnglish = nameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FamilyCompositionId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

