using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultSoftSkills;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorSoftSkills;

public sealed class CollaboratorSoftSkill : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorSoftSkill()
    {
    }

    public CollaboratorSoftSkill(CollaboratorSoftSkillId id, CollaboratorId collaboratorId, DefaultSoftSkillId? defaultSoftSkillId, string otherLanguageName, string otherLanguageNameEnglish, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        DefaultSoftSkillId = defaultSoftSkillId;

        OtherLanguageName = otherLanguageName;
        OtherLanguageNameEnglish = otherLanguageNameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorSoftSkillId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public DefaultSoftSkillId? DefaultSoftSkillId { get; set; }
    public DefaultSoftSkill? DefaultSoftSkill { get; set; }

    public string? OtherLanguageName { get; set; } = string.Empty;
    public string? OtherLanguageNameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

