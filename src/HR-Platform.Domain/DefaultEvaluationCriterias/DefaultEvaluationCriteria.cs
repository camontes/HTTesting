using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.DefaultEvaluationCriterias;

public sealed class DefaultEvaluationCriteria : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DefaultEvaluationCriteria()
    {
    }

    public DefaultEvaluationCriteria(DefaultEvaluationCriteriaId id, int evaluationCriteriaTypeId, string name, string nameEnglish, string description, string descriptionEnglish, int percentage,
        bool isEditable, bool isDeleteable)
    {
        Id = id;

        EvaluationCriteriaTypeId = evaluationCriteriaTypeId;

        Name = name;
        NameEnglish = nameEnglish;

        Description = description;
        DescriptionEnglish = descriptionEnglish;

        Percentage = percentage;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DefaultEvaluationCriteriaId Id { get; set; }

    public int EvaluationCriteriaTypeId { get; set; }

    public string Name { get; set; }
    public string NameEnglish { get; set; }

    public string Description { get; set; }
    public string DescriptionEnglish { get; set; }

    public int Percentage { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }
}

