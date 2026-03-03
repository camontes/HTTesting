using HR_Platform.Domain.DefaultEvaluationCriterias;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultEvaluationCriteriaScores;

public sealed class DefaultEvaluationCriteriaScore : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DefaultEvaluationCriteriaScore()
    {
    }

    public DefaultEvaluationCriteriaScore(DefaultEvaluationCriteriaScoreId id, int defaultEvaluationCriteriaId, string description, string descriptionEnglish, 
        int lowerScore, int upperScore, bool isForDefaultCriterias,
        bool isEditable, bool isDeleteable)
    {
        Id = id;

        DefaultEvaluationCriteriaId = defaultEvaluationCriteriaId;

        Description = description;
        DescriptionEnglish = descriptionEnglish;

        LowerScore = lowerScore;
        UpperScore = upperScore;

        IsForDefaultCriterias = isForDefaultCriterias;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DefaultEvaluationCriteriaScoreId Id { get; set; }

    public int DefaultEvaluationCriteriaId { get; set; }

    public string Description { get; set; }
    public string DescriptionEnglish { get; set; }

    public int LowerScore { get; set; }
    public int UpperScore { get; set; }

    public bool IsForDefaultCriterias { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }
}

