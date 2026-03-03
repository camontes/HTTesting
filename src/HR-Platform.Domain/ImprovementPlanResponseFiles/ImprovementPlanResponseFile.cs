using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.ImprovementPlanResponseFiles;

public sealed class ImprovementPlanResponseFile : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private ImprovementPlanResponseFile()
    {
    }

    public ImprovementPlanResponseFile(ImprovementPlanResponseFileId id, ImprovementPlanResponseId improvementPlanResponseId,
        string fileName, string urlFile, string emailWhoChanged, string nameWhoChanged, string urlPhotoWhoChanged,
        bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        ImprovementPlanResponseId = improvementPlanResponseId;

        FileName = fileName;
        UrlFile = urlFile;
        EmailWhoChanged = emailWhoChanged;
        NameWhoChanged = nameWhoChanged;
        UrlPhotoWhoChanged = urlPhotoWhoChanged;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public ImprovementPlanResponseFileId Id { get; set; }

    public ImprovementPlanResponseId ImprovementPlanResponseId { get; set; }
    public ImprovementPlanResponse ImprovementPlanResponse { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string UrlFile { get; set; } = string.Empty;
    public string EmailWhoChanged { get; set; } = string.Empty;
    public string NameWhoChanged { get; set; } = string.Empty;
    public string UrlPhotoWhoChanged { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}


