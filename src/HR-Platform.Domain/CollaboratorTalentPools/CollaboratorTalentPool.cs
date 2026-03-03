using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorTalentPools;

public sealed class CollaboratorTalentPool : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorTalentPool()
    {
    }

    public CollaboratorTalentPool
    (
        CollaboratorTalentPoolId id,

        CollaboratorId collaboratorId, 
        TalentPoolId tagId,

        TimeDate creationDate, TimeDate editionDate
        )
    {
        Id = id;

        CollaboratorId = collaboratorId;

        TalentPoolId = tagId;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

    #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorTalentPoolId Id { get; set; }
        
    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }


    public TalentPoolId TalentPoolId { get; set; } 
    public TalentPool TalentPool { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

