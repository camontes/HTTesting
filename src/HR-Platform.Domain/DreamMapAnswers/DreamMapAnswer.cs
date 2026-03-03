using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.DreamMapAnswers;

public sealed class DreamMapAnswer : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DreamMapAnswer()
    {
    }

    public DreamMapAnswer(DreamMapAnswerId id, CollaboratorDreamMapAnswerId collaboratorDreamMapAnswerId, string question, string answer, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorDreamMapAnswerId = collaboratorDreamMapAnswerId;
        Question = question;
        Answer = answer;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DreamMapAnswerId Id { get; set; }

    public CollaboratorDreamMapAnswer CollaboratorDreamMapAnswer { get; set; }
    public CollaboratorDreamMapAnswerId CollaboratorDreamMapAnswerId { get; set; }

    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

