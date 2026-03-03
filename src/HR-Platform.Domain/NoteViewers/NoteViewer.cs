using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.NoteViewers;

public sealed class NoteViewer : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private NoteViewer()
    {
    }

    public NoteViewer(NoteViewerId id, CollaboratorId viewerId, NoteId noteId, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        ViewerId = viewerId;
        NoteId = noteId;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public NoteViewerId Id { get; set; }

    public NoteId NoteId { get; set; }
    public Note Note { get; set; }

    public CollaboratorId ViewerId { get; set; }
    public Collaborator Viewer { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

