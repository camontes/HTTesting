using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorCriteriaAnswers;

public sealed class CollaboratorCriteriaAnswer : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorCriteriaAnswer()
    {
    }

    public CollaboratorCriteriaAnswer(
        CollaboratorCriteriaAnswerId id, EvaluationCriteriaTypeId evaluationCriteriaTypeId, int generalObjetiveCriteriaPercentage, int generalSubjetiveCriteriaPercentage,
        CollaboratorCriteriaId collaboratorCriteriaId, string criteriaName, string criteriaNameEnglish, int criteriaPercentage, string criteriaScoreName, 
        string criteriaScoreNameEnglish, int criteriaScorePercentage, int criteriaScoreIndexAnswerr,
        string referenceNumber, string position, string positionEnglish, string comments, bool isInHistorical, bool isEditable,
        bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        EvaluationCriteriaTypeId = evaluationCriteriaTypeId;
        GeneralObjetiveCriteriaPercentage = generalObjetiveCriteriaPercentage;
        GeneralSubjetiveCriteriaPercentage = generalSubjetiveCriteriaPercentage;

        CollaboratorCriteriaId = collaboratorCriteriaId;

        CriteriaName = criteriaName;
        CriteriaNameEnglish = criteriaNameEnglish;

        CriteriaScoreName = criteriaScoreName;
        CriteriaScoreNameEnglish = criteriaScoreNameEnglish;
        CriteriaScorePercentage = criteriaScorePercentage;
        CriteriaScoreIndexAnswerr = criteriaScoreIndexAnswerr;

        ReferenceNumber = referenceNumber;

        CriteriaPercentage = criteriaPercentage;

        Position = position;
        PositionEnglish = positionEnglish;


        Comments = comments;

        IsInHistorical = isInHistorical;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorCriteriaAnswerId Id { get; set; }
    public string ReferenceNumber { get; set; }

    public EvaluationCriteriaTypeId EvaluationCriteriaTypeId { get; set; }
    public EvaluationCriteriaType EvaluationCriteriaType { get; set; }

    public int GeneralObjetiveCriteriaPercentage { get; set; }
    public int GeneralSubjetiveCriteriaPercentage { get; set; }

    public CollaboratorCriteriaId CollaboratorCriteriaId { get; set; }
    public CollaboratorCriteria CollaboratorCriteria { get; set; }

    //public PriorityNovelty PriorityNovelty { get; set; }
    //public PriorityNoveltyId PriorityNoveltyId { get; set; }
    public int PriorityNoveltyId { get; set; }

    public string CriteriaName { get; set; }
    public string CriteriaNameEnglish { get; set; }
    public int CriteriaPercentage { get; set; }


    public string CriteriaScoreName { get; set; }
    public string CriteriaScoreNameEnglish { get; set; }
    public int CriteriaScorePercentage { get; set; }
    public int CriteriaScoreIndexAnswerr { get; set; }


    public string Position { get; set; }
    public string PositionEnglish { get; set; }

    public string Comments { get; set; }

    public bool IsInHistorical{ get; set; } = false;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<ImprovementPlan> ImprovementPlans { get; set; }
}

