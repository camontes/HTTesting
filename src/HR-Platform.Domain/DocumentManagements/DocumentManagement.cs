using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultFileTypes;
using HR_Platform.Domain.DocumentManagementFileTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.DocumentManagements;

public sealed class DocumentManagement : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DocumentManagement()
    {
    }

    public DocumentManagement(DocumentManagementId id, CollaboratorId collaboratorId, string fileName, string urlFile, string emailWhoChangedByTH, string nameWhoChangedByTH, string urlPhotoWhoChangedByTH, DocumentManagementFileTypeId documentManagementFileTypeId, string other, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        FileName = fileName;
        UrlFile = urlFile;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;
        UrlPhotoWhoChangedByTH = urlPhotoWhoChangedByTH;

        DocumentManagementFileTypeId = documentManagementFileTypeId;
        Other = other;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DocumentManagementId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public DocumentManagementFileTypeId DocumentManagementFileTypeId { get; set; }
    public DocumentManagementFileType DocumentManagementFileType { get; set; }

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

