using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultLanguageLevels;
using HR_Platform.Domain.DefaultLanguageTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorLanguages;

public sealed class CollaboratorLanguage : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorLanguage()
    {
    }

    public CollaboratorLanguage(CollaboratorLanguageId id, CollaboratorId collaboratorId, DefaultLanguageLevelId? defaultLanguageLevelId, 
        DefaultLanguageTypeId? defaultLanguageTypeId, string otherLanguageName, string otherLanguageNameEnglish, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        DefaultLanguageLevelId = defaultLanguageLevelId;
        DefaultLanguageTypeId = defaultLanguageTypeId;

        OtherLanguageName = otherLanguageName;
        OtherLanguageNameEnglish = otherLanguageNameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorLanguageId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public DefaultLanguageLevelId? DefaultLanguageLevelId { get; set; }
    public DefaultLanguageLevel? DefaultLanguageLevel { get; set; }

    public DefaultLanguageTypeId? DefaultLanguageTypeId { get; set; }
    public DefaultLanguageType? DefaultLanguageType { get; set; }

    public string? OtherLanguageName { get; set; } = string.Empty;
    public string? OtherLanguageNameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

