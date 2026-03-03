using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultEducationStages;
using HR_Platform.Domain.DefaultProfessions;
using HR_Platform.Domain.DefaultStudyAreas;
using HR_Platform.Domain.DefaultStudyTypes;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorEducations;

public sealed class CollaboratorEducation : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorEducation()
    {
    }

    public CollaboratorEducation(CollaboratorEducationId id, CollaboratorId collaboratorId, string institutionName,
        DefaultProfessionId defaultProfessionId, string otherProfessionName, EducationalLevelId? educationalLevelId, DefaultStudyTypeId? defaultStudyTypeId,
        bool isCertificated, DefaultStudyAreaId? defaultStudyAreaId, bool isCompletedStudy,
        TimeDate? endEducationDate, TimeDate? startEducationDate, DefaultEducationStageId? defaultEducationStageId,
        string? educationFileURL, string? educationFileName, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        InstitutionName = institutionName;

        DefaultProfessionId = defaultProfessionId;
        OtherProfessionName = otherProfessionName;
        EducationalLevelId = educationalLevelId;
        DefaultStudyTypeId = defaultStudyTypeId;

        IsCertificated = isCertificated;

        DefaultStudyAreaId = defaultStudyAreaId;

        IsCompletedStudy = isCompletedStudy;


        EndEducationDate = endEducationDate;
        StartEducationDate = startEducationDate;

        DefaultEducationStageId = defaultEducationStageId;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
        EducationFileURL = educationFileURL;
        EducationFileName = educationFileName;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorEducationId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public string InstitutionName { get; set; } = string.Empty;

    public DefaultProfessionId DefaultProfessionId { get; set; }
    public DefaultProfession DefaultProfession { get; set; }
    public string OtherProfessionName { get; set; }

    public EducationalLevelId? EducationalLevelId { get; set; }
    public EducationalLevel EducationalLevel { get; set; }

    public DefaultStudyTypeId? DefaultStudyTypeId { get; set; }
    public DefaultStudyType DefaultStudyType { get; set; }

    public bool IsCertificated { get; set; }

    public DefaultStudyAreaId? DefaultStudyAreaId { get; set; }
    public DefaultStudyArea DefaultStudyArea { get; set; }

    public bool IsCompletedStudy { get; set; }

    public TimeDate? StartEducationDate { get; set; }
    public TimeDate? EndEducationDate { get; set; }

    public DefaultEducationStageId? DefaultEducationStageId { get; set; }
    public DefaultEducationStage? DefaultEducationStage { get; set; }

    public string? EducationFileURL { get; set; } = string.Empty;

    public string? EducationFileName { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

