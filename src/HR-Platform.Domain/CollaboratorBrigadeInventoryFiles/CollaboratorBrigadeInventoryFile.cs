using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;

public sealed class CollaboratorBrigadeInventoryFile : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorBrigadeInventoryFile()
    {
    }

    public CollaboratorBrigadeInventoryFile(CollaboratorBrigadeInventoryFileId id, CollaboratorBrigadeInventoryId inductionId, string fileName, string urlFile, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorBrigadeInventoryId = inductionId;

        FileName = fileName;
        UrlFile = urlFile;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorBrigadeInventoryFileId Id { get; set; }

    public CollaboratorBrigadeInventoryId CollaboratorBrigadeInventoryId { get; set; }
    public CollaboratorBrigadeInventory CollaboratorBrigadeInventory { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string UrlFile { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

