using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.ImprovementPlanTaskFiles;

public sealed class ImprovementPlanTaskFile : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private ImprovementPlanTaskFile()
    {
    }

    public ImprovementPlanTaskFile(ImprovementPlanTaskFileId id, ImprovementPlanTaskId improvementPlanId, string fileName, string urlFile, string emailWhoChangedByTH, string nameWhoChangedByTH, string urlPhotoWhoChangedByTH, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        ImprovementPlanTaskId = improvementPlanId;

        FileName = fileName;
        UrlFile = urlFile;
        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;
        UrlPhotoWhoChangedByTH = urlPhotoWhoChangedByTH;


        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public ImprovementPlanTaskFileId Id { get; set; }

    public ImprovementPlanTaskId ImprovementPlanTaskId { get; set; }
    public ImprovementPlanTask ImprovementPlanTask { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string UrlFile { get; set; } = string.Empty;
    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;
    public string UrlPhotoWhoChangedByTH { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

