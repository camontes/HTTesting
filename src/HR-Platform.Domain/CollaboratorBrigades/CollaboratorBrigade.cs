using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorBrigades;

public sealed class CollaboratorBrigade : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorBrigade()
    {
    }

    public CollaboratorBrigade(CollaboratorBrigadeId id, CollaboratorBrigadeInventoryId collaboratorBrigadeInventoryId,
        CollaboratorId collaboratorId, BrigadeAdjustmentId brigadeAdjustmentId,
        int amountByBrigader, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorBrigadeInventoryId = collaboratorBrigadeInventoryId;

        CollaboratorId = collaboratorId;
        BrigadeAdjustmentId = brigadeAdjustmentId;

        AmountByBrigader = amountByBrigader;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorBrigadeId Id { get; set; }

    public CollaboratorBrigadeInventoryId CollaboratorBrigadeInventoryId { get; set; }
    public CollaboratorBrigadeInventory CollaboratorBrigadeInventory { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public BrigadeAdjustmentId BrigadeAdjustmentId { get; set; }
    public BrigadeAdjustment BrigadeAdjustment { get; set; }

    public int AmountByBrigader { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

