using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorTags;

public sealed class CollaboratorTag : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorTag()
    {
    }

    public CollaboratorTag
    (
        CollaboratorTagId id,

        CollaboratorId collaboratorId, 
        TagId tagId,

        TimeDate creationDate, TimeDate editionDate
        )
    {
        Id = id;

        CollaboratorId = collaboratorId;

        TagId = tagId;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

    #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorTagId Id { get; set; }
        
    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public TagId TagId { get; set; }
    public Tag Tag { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

