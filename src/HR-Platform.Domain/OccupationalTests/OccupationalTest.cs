using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultFileTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.OccupationalTests;

public sealed class OccupationalTest : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private OccupationalTest()
    {
    }

    public OccupationalTest(OccupationalTestId id, CollaboratorId collaboratorId, string fileName, string urlFile, string emailWhoChangedByTH, string nameWhoChangedByTH, string urlPhotoWhoChangedByTH, DefaultFileTypeId defaulFileTypeId, string other, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        FileName = fileName;
        UrlFile = urlFile;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;
        UrlPhotoWhoChangedByTH = urlPhotoWhoChangedByTH;

        DefaultFileTypeId = defaulFileTypeId;
        Other = other;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public OccupationalTestId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public DefaultFileTypeId DefaultFileTypeId { get; set; }
    public DefaultFileType DefaultFileType { get; set; }

    public string Other { get; set; } = string.Empty;
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

