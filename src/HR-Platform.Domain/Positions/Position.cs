using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Positions;

public sealed class Position : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Position()
    {
    }

    public Position(PositionId id, CompanyId companyId, string name, string nameEnglish, string description, string descriptionEnglish, string positionFile, string positionFileName,
        int objectiveCriteria, int subjectiveCriteria,
        bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate, TimeDate criteriasEditionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;
        NameEnglish = nameEnglish;

        Description = description;
        DescriptionEnglish = descriptionEnglish;

        PositionFile = positionFile;
        PositionFileName = positionFileName;

        ObjectiveCriteria = objectiveCriteria;
        SubjectiveCriteria = subjectiveCriteria;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
        CriteriasEditionDate = criteriasEditionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public PositionId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string DescriptionEnglish { get; set; } = string.Empty;

    public string PositionFile { get; set; } = string.Empty;
    public string PositionFileName { get; set; } = string.Empty;

    public int ObjectiveCriteria { get; set; }
    public int SubjectiveCriteria { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
    public TimeDate CriteriasEditionDate { get; set; }

    public List<Collaborator> Collaborators { get; set; }
    public List<EvaluationCriteria> EvaluationCriterias { get; set; }
    public List<CollaboratorCriteria> CollaboratorCriterias { get; set; }
}

