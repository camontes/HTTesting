using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.UnitMeasures;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorBrigadeInventories;

public sealed class CollaboratorBrigadeInventory : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorBrigadeInventory()
    {
    }

    public CollaboratorBrigadeInventory(CollaboratorBrigadeInventoryId id, CompanyId companyId, BrigadeInventoryId brigadeInventoryId,
        bool sendForAll, int quantityDelivered, TimeDate deliveryDate, TimeDate returnDate, UnitMeasureId unitMeasureId, 
        string observations, string emailWhoChangedByTH, string nameWhoChangedByTH, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        BrigadeInventoryId = brigadeInventoryId;

        SendForAll = sendForAll;

        QuantityDelivered = quantityDelivered;
        UnitMeasureId = unitMeasureId;
        DeliveryDate = deliveryDate;
        ReturnDate = returnDate;

        Observations = observations;
        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;

    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorBrigadeInventoryId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }
    public BrigadeInventory BrigadeInventory { get; set; }
    public BrigadeInventoryId BrigadeInventoryId { get; set; }
    public bool SendForAll { get; set; }
    public int QuantityDelivered { get; set; }
    public UnitMeasure UnitMeasure { get; set; }
    public UnitMeasureId UnitMeasureId { get; set; }

    public TimeDate DeliveryDate { get; set; }
    public TimeDate ReturnDate { get; set; }
    
    public string Observations { get; set; } = string.Empty;

    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<CollaboratorBrigadeInventoryFile> CollaboratorBrigadeInventoryFiles { get; set; }
    public List<CollaboratorBrigade> CollaboratorBrigades { get; set; }
}

