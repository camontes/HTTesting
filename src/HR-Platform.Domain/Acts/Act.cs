using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Acts;

public sealed class Act : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Act()
    {
    }

    public Act(ActId id, CompanyId companyId, CollaboratorId collaboratorId, string file, string fileName, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;
        CompanyId = companyId;
        CollaboratorId = collaboratorId;

        File = file;
        FileName = fileName;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public ActId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public string File { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

