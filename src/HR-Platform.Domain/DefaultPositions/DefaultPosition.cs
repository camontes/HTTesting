using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultPositions;

public sealed class DefaultPosition : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DefaultPosition()
    {
    }

    public DefaultPosition(DefaultPositionId id, string name, string nameEnglish, string description, string descriptionEnglish)
    {
        Id = id;

        Name = name;
        NameEnglish = nameEnglish;

        Description = description;
        DescriptionEnglish = descriptionEnglish;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DefaultPositionId Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string DescriptionEnglish { get; set; } = string.Empty;

}

