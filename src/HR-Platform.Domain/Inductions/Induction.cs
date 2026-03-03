using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Inductions;

public sealed class Induction : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Induction()
    {
    }

    public Induction(InductionId id, CompanyId companyId, string name, string description, string emailWhoChangedByTH,
        string nameWhoChangedByTH, bool isVisible, TimeDate isVisibleChangeDate, bool allowForAllCollaborators,
        string emailWhoDeletedByTH, bool isInductionDeleted, TimeDate deleteDate, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate
        )
    {
        Id = id;

        CompanyId = companyId;

        Name = name;
        Description = description;
        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;
        IsVisible = isVisible;
        IsVisibleChangeDate = isVisibleChangeDate;
        AllowForAllCollaborators = allowForAllCollaborators;

        EmailWhoDeletedByTH = emailWhoDeletedByTH;
        IsInductionDeleted = isInductionDeleted;
        DeleteDate = deleteDate;


        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public InductionId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public TimeDate IsVisibleChangeDate { get; set; }

    public bool AllowForAllCollaborators { get; set; }

    public string EmailWhoDeletedByTH { get; set; } = string.Empty;
    public bool IsInductionDeleted { get; set; } = false;
    public TimeDate DeleteDate { get; set; }


    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }


    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<CollaboratorGeneralInduction> CollaboratorGeneralInductions { get; set; }
    public List<CollaboratorInduction> CollaboratorInductions { get; set; }
    public List<InductionFile> InductionFiles { get; set; }
}

