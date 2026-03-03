using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.NoteViewers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Notes;

public sealed class Note : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Note()
    {
    }

    public Note(NoteId id, string description, CollaboratorId createdBy, CollaboratorId assignedTo, NoteId? parentNoteId, bool isPublic, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        Description = description;
        CreatedBy = createdBy;
        AssignedTo = assignedTo;
        ParentNoteId = parentNoteId;
        IsPublic = isPublic;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public NoteId Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public CollaboratorId CreatedBy { get; set; }
    public Collaborator Creator { get; set; }

    public CollaboratorId AssignedTo { get; set; }
    public Collaborator Assignee { get; set; }

    public NoteId? ParentNoteId { get; set; }
    public Note ParentNote { get; set; }

    public bool IsPublic { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<Note> Replies { get; set; }
    public List<NoteViewer> Viewers { get; set; }
    public List<NoteFile> NoteFiles { get; set; }

}

