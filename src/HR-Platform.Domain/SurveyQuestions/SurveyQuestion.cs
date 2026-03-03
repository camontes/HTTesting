using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.SurveyQuestionMandatoryTypes;
using HR_Platform.Domain.SurveyQuestionTypes;
using HR_Platform.Domain.Surveys;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.SurveyQuestions;

public sealed class SurveyQuestion : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private SurveyQuestion()
    {
    }

    public SurveyQuestion(SurveyQuestionId id, SurveyId surveyId, SurveyQuestionTypeId surveyQuestionTypeId, SurveyQuestionMandatoryTypeId surveyQuestionMandatoryTypeId,
        string text, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        SurveyId = surveyId;
        SurveyQuestionTypeId = surveyQuestionTypeId;
        SurveyQuestionMandatoryTypeId = surveyQuestionMandatoryTypeId;

        Text = text;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public SurveyQuestionId Id { get; set; }

    public SurveyId SurveyId { get; set; }
    public Survey Survey { get; set; }

    public SurveyQuestionTypeId SurveyQuestionTypeId { get; set; }
    public SurveyQuestionType SurveyQuestionType { get; set; }

    public SurveyQuestionMandatoryTypeId SurveyQuestionMandatoryTypeId { get; set; }
    public SurveyQuestionMandatoryType SurveyQuestionMandatoryType { get; set; }

    public string Text { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

