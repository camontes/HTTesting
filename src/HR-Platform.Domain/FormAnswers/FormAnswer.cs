using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.FormAnswers;

public sealed class FormAnswer : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private FormAnswer()
    {
    }

    public FormAnswer(FormAnswerId id, FormQuestionsTypeId formQuestionsTypeId, CollaboratorId collaboratorId, FormAnswerGroupId formAnswerGroupId,
        string answer, string referenceNumber, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        FormQuestionsTypeId = formQuestionsTypeId;
        FormAnswerGroupId = formAnswerGroupId;
        CollaboratorId = collaboratorId;

        Answer = answer;
        ReferenceNumber = referenceNumber;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FormAnswerId Id { get; set; }

    public FormQuestionsTypeId FormQuestionsTypeId { get; set; }
    public FormQuestionsType FormQuestionsType { get; set; }

    public FormAnswerGroupId FormAnswerGroupId { get; set; }
    public FormAnswerGroup FormAnswerGroup { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public string Answer { get; set; } = string.Empty; // Short Text, Long Text, Only Option etc. For multi-option an extra table should be created.
    public string ReferenceNumber { get; set; } = string.Empty;
   
    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

