using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.UnitMeasures;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.CollaboratorBrigadeInventories;

namespace HR_Platform.Domain.BrigadeInventories;

public sealed class BrigadeInventory : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private BrigadeInventory()
    {
    }

    public BrigadeInventory(BrigadeInventoryId id, CompanyId companyId, string name, string description,
        string companyLocation, int amount, int availableAmount, UnitMeasureId unitMeasureId, TimeDate purchaseDate,
        TimeDate expirationDate, string observations, string emailWhoChangedByTH, string nameWhoChangedByTH,
        bool isEditable, bool isDeleteable, bool isDeleted, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;
        Description = description;
        CompanyLocation = companyLocation;
        Amount = amount;
        AvailableAmount = availableAmount;
        UnitMeasureId = unitMeasureId;
        PurchaseDate = purchaseDate;
        ExpirationDate = expirationDate;
        Observations = observations;
        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        IsDeleted = isDeleted;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BrigadeInventoryId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CompanyLocation { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int AvailableAmount { get; set; }
    public UnitMeasure UnitMeasure { get; set; }
    public UnitMeasureId UnitMeasureId { get; set; }
    public string Other { get; set; } = string.Empty;

    public TimeDate PurchaseDate { get; set; }
    public TimeDate ExpirationDate { get; set; }
    
    public string Observations { get; set; } = string.Empty;

    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public bool IsDeleted { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<BrigadeInventoryFile> BrigadeInventoryFiles { get; set; }
    public List<CollaboratorBrigadeInventory> CollaboratorBrigadeInventories { get; set; }
}

