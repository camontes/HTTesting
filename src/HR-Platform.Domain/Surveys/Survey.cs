using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.SurveyQuestions;
using HR_Platform.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace HR_Platform.Domain.Surveys;

public sealed class Survey : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Survey()
    {
    }

    public Survey(SurveyId id, CompanyId companyId, AreaId surveyTypeId, string name, string description, string emailWhoChangedByTH, string nameWhoChangedByTH,
        bool isVisible, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;
        SurveyTypeId = surveyTypeId;

        Name = name;

        Description = description;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsVisible = isVisible;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public SurveyId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public AreaId SurveyTypeId { get; set; }
    [JsonIgnore]
    public Area SurveyType { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;

    public bool IsVisible { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<SurveyQuestion> SurveyQuestions { get; set; }
}

