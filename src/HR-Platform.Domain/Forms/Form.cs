using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Forms;

public sealed class Form : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Form()
    {
    }

    public Form(FormId id, CompanyId companyId, string name, string description, AreaId noveltyTypeId, bool isVisible, 
        bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;
        Description = description;

        NoveltyTypeId = noveltyTypeId;

        IsVisible = isVisible;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public FormId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public AreaId NoveltyTypeId { get; set; }
    public Area NoveltyType { get; set; }

    public bool IsVisible { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<FormQuestionsType> FormQuestionsTypes { get; set; }
    public List<FormAnswerGroup> FormAnswerGroups { get; set; }
}

