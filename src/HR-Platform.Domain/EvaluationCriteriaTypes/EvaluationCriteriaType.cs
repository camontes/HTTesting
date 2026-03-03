using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.EvaluationCriteriaTypes;

public sealed class EvaluationCriteriaType : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private EvaluationCriteriaType()
    {
    }

    public EvaluationCriteriaType(EvaluationCriteriaTypeId id, string name, string nameEnglish, int value, bool isEditable, bool isDeleteable)
    {
        Id = id;

        Name = name;
        NameEnglish = nameEnglish;

        Value = value;
        
        IsEditable = isEditable;
        IsDeleteable = isDeleteable;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public EvaluationCriteriaTypeId Id { get; set; }

    public string Name { get; set; }
    public string NameEnglish { get; set; }

    public int Value { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public List<EvaluationCriteria> EvaluationCriterias { get; set; }
    public List<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswers { get; set; }
}

