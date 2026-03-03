using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.SurveyQuestions;

namespace HR_Platform.Domain.SurveyQuestionMandatoryTypes;

public sealed class SurveyQuestionMandatoryType : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private SurveyQuestionMandatoryType()
    {
    }

    public SurveyQuestionMandatoryType(SurveyQuestionMandatoryTypeId id, string name, string nameEnglish, bool isEditable, bool isDeleteable)
    {
        Id = id;

        Name = name;
        NameEnglish = nameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public SurveyQuestionMandatoryTypeId Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public List<SurveyQuestion> SurveyQuestions { get; set; }
}

