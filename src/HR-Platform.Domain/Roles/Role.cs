 using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.RolesPermissions;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.EvaluationCriterias;

namespace HR_Platform.Domain.Roles;

public sealed class Role : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private Role()
    {
    }

    public Role(RoleId id, CompanyId companyId, string name, string nameEnglish, AreaId areaId, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;
        NameEnglish = nameEnglish;

        AreaId = areaId;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public RoleId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;

    public AreaId AreaId { get; set; }
    public Area Area { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<Collaborator> Collaborators { get; set; }
    public List<RolePermission> RolesPermissions { get; set; }

}

