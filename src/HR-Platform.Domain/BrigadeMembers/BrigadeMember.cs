using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.BrigadeMembers;

public sealed class BrigadeMember : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private BrigadeMember()
    {
    }

    public BrigadeMember(BrigadeMemberId id, CollaboratorId collaboratorId, BrigadeAdjustmentId brigadeAdjustmentId, bool hasBeenDeletedBrigadeAdjustment, bool isMainLeader, bool isBrigadeLeader, bool isVisible, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;
        BrigadeAdjustmentId = brigadeAdjustmentId;
        HasBeenDeletedBrigadeAdjustment = hasBeenDeletedBrigadeAdjustment;

        IsMainLeader = isMainLeader;
        IsBrigadeLeader = isBrigadeLeader;

        IsVisible = isVisible;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BrigadeMemberId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public BrigadeAdjustmentId BrigadeAdjustmentId { get; set; }
    public BrigadeAdjustment BrigadeAdjustment { get; set; }
    public bool HasBeenDeletedBrigadeAdjustment { get; set; }

    public bool IsMainLeader {  get; set; }
    public bool IsBrigadeLeader {  get; set; }
    public bool IsVisible { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

