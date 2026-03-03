using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.UnitMeasures;

public sealed class UnitMeasure : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private UnitMeasure()
    {
    }

    public UnitMeasure(UnitMeasureId id, string name, string nameEnglish, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        Name = name;
        NameEnglish = nameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public UnitMeasureId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<BrigadeInventory> BrigadeInventories { get; set; }
    public List<CollaboratorBrigadeInventory> CollaboratorBrigadeInventories { get; set; }
}

