using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.AssignationTypes;

public sealed class AssignationType : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private AssignationType()
    {
    }

    public AssignationType(AssignationTypeId id, string name, string nameEnglish)
    {
        Id = id;
        Name = name;
        NameEnglish = nameEnglish;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public AssignationTypeId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public List<Collaborator> Collaborators { get; set; }
}

