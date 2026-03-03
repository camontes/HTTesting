using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultLifePreferences;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorLifePreferences;

public sealed class CollaboratorLifePreference : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorLifePreference()
    {
    }

    public CollaboratorLifePreference(CollaboratorLifePreferenceId id, CollaboratorId collaboratorId, DefaultLifePreferenceId? defaultLifePreferenceId, string otherLanguageName, string otherLanguageNameEnglish, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CollaboratorId = collaboratorId;

        DefaultLifePreferenceId = defaultLifePreferenceId;

        OtherLanguageName = otherLanguageName;
        OtherLanguageNameEnglish = otherLanguageNameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorLifePreferenceId Id { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public DefaultLifePreferenceId? DefaultLifePreferenceId { get; set; }
    public DefaultLifePreference? DefaultLifePreference { get; set; }

    public string? OtherLanguageName { get; set; } = string.Empty;
    public string? OtherLanguageNameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

