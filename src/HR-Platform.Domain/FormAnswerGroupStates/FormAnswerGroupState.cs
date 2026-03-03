using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.FormAnswerGroupStates;

public sealed class FormAnswerGroupState : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private FormAnswerGroupState()
    {
    }

    public FormAnswerGroupState(FormAnswerGroupStateId id, string name, string nameEnglish, bool isEditable, bool isDeleteable)
    {
        Id = id;

        Name = name;
        NameEnglish = nameEnglish;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FormAnswerGroupStateId Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public List<FormAnswerGroup> FormAnswerGroups { get; set; }
}

