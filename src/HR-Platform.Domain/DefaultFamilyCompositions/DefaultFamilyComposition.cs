using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.DefaultFamilyCompositions;

public sealed class DefaultFamilyComposition : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private DefaultFamilyComposition()
    {
    }

    public DefaultFamilyComposition(DefaultFamilyCompositionId id, string name, string nameEnglish)
    {
        Id = id;
        Name = name;
        NameEnglish = nameEnglish;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public DefaultFamilyCompositionId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

}

