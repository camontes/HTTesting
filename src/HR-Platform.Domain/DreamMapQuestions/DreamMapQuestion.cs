using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.DreamMapQuestions;

public sealed class DreamMapQuestion : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private DreamMapQuestion()
    {
    }

    public DreamMapQuestion(DreamMapQuestionId id, CompanyId companyId, string question, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Question = question;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DreamMapQuestionId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Question { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<DreamMapAnswer> DreamMapAnswers { get; set; }
}

