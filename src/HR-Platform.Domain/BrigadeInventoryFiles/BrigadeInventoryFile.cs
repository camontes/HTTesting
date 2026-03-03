using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.BrigadeInventoryFiles;

public sealed class BrigadeInventoryFile : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private BrigadeInventoryFile()
    {
    }

    public BrigadeInventoryFile(BrigadeInventoryFileId id, BrigadeInventoryId inductionId, string fileName, string urlFile, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        BrigadeInventoryId = inductionId;

        FileName = fileName;
        UrlFile = urlFile;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BrigadeInventoryFileId Id { get; set; }

    public BrigadeInventoryId BrigadeInventoryId { get; set; }
    public BrigadeInventory BrigadeInventory { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string UrlFile { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

