using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.EvaluationCriterias;

public sealed class EvaluationCriteria : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private EvaluationCriteria()
    {
    }

    public EvaluationCriteria(EvaluationCriteriaId id, EvaluationCriteriaTypeId evaluationCriteriaTypeId, PositionId positionId, 
        string name, string nameEnglish, string description, string descriptionEnglish, int percentage,
        bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        EvaluationCriteriaTypeId = evaluationCriteriaTypeId;

        PositionId = positionId;
                
        Name = name;
        NameEnglish = nameEnglish;

        Description = description;
        DescriptionEnglish = descriptionEnglish;

        Percentage = percentage;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public EvaluationCriteriaId Id { get; set; }

    public EvaluationCriteriaTypeId EvaluationCriteriaTypeId {  get; set; }
    public EvaluationCriteriaType EvaluationCriteriaType {  get; set; }

    public PositionId PositionId { get; set; }
    public Position Position { get; set; }

    public string Name { get; set; }
    public string NameEnglish { get; set; }

    public string Description { get; set; }
    public string DescriptionEnglish { get; set; }

    public int Percentage { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<EvaluationCriteriaScore> EvaluationCriteriaScores { get; set; }
}

