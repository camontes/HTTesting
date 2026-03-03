using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorDreamMapAnswers;

public sealed class CollaboratorDreamMapAnswer : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorDreamMapAnswer()
    {
    }

    public CollaboratorDreamMapAnswer(CollaboratorDreamMapAnswerId id, CollaboratorId collaboratorId, int templateIndicator, bool saveCurrent,  bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;


        CollaboratorId = collaboratorId;

        TemplateIndicator = templateIndicator;

        SaveCurrent = saveCurrent;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorDreamMapAnswerId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public int TemplateIndicator { get; set; }

    public bool SaveCurrent { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
    public List<DreamMapAnswer> DreamMapAnswers { get; set; }

}

