using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.QuestionTypes;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.FormQuestionsTypes;

public sealed class FormQuestionsType : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private FormQuestionsType()
    {
    }

    public FormQuestionsType(FormQuestionsTypeId id, FormId formId, QuestionTypeId questionTypeId, string question, bool isRequired, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        FormId = formId;
        QuestionTypeId = questionTypeId;

        Question = question;
        IsRequired = isRequired;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FormQuestionsTypeId Id { get; set; }

    public FormId FormId { get; set; }
    public Form Form { get; set; }

    public string Question { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public QuestionTypeId QuestionTypeId { get; set; }
    public QuestionType QuestionType { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<FormAnswer> FormAnswers { get; set; }

}

