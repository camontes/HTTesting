using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.EvaluationCriteriaScores;

public sealed class EvaluationCriteriaScore : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private EvaluationCriteriaScore()
    {
    }

    public EvaluationCriteriaScore(EvaluationCriteriaScoreId id, EvaluationCriteriaId evaluationCriteriaId, 
        string description, string descriptionEnglish, int lowerScore, int upperScore, int indexScoreAnswer,
        bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        EvaluationCriteriaId = evaluationCriteriaId;

        Description = description;
        DescriptionEnglish = descriptionEnglish;

        LowerScore = lowerScore;
        UpperScore = upperScore;

        IndexScoreAnswer = indexScoreAnswer;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public EvaluationCriteriaScoreId Id { get; set; }

    public EvaluationCriteriaId EvaluationCriteriaId { get; set; }
    public EvaluationCriteria EvaluationCriteria { get; set; }

    public string Description { get; set; }
    public string DescriptionEnglish { get; set; }

    public int LowerScore { get; set; }
    public int UpperScore { get; set; }

    public int IndexScoreAnswer { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }


}

