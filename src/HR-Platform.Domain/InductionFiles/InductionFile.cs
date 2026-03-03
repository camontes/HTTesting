using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.InductionFiles;

public sealed class InductionFile : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private InductionFile()
    {
    }

    public InductionFile(InductionFileId id, InductionId inductionId, string fileName, string urlFile, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        InductionId = inductionId;

        FileName = fileName;
        UrlFile = urlFile;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public InductionFileId Id { get; set; }

    public InductionId InductionId { get; set; }
    public Induction Induction { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string UrlFile { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

