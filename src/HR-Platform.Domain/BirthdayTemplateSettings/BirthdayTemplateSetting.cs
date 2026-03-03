using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.BirthdayTemplateSettings;

public sealed class BirthdayTemplateSetting : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private BirthdayTemplateSetting()
    {
    }

    public BirthdayTemplateSetting(BirthdayTemplateSettingId id, CompanyId companyId, bool isDefaultTemplate, bool isAllowSendPost, string customMessage, string customTemplateURL, string customTemplateName, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        IsDefaultTemplate = isDefaultTemplate;
        IsAllowSendPost = isAllowSendPost;

        CustomMessage = customMessage;
        CustomTemplateURL = customTemplateURL;
        CustomTemplateName = customTemplateName;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BirthdayTemplateSettingId Id { get; set; }
    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public bool IsDefaultTemplate {  get; set; }
    public bool IsAllowSendPost {  get; set; }

    public string CustomMessage { get; set; }
    public string CustomTemplateURL { get; set; }
    public string CustomTemplateName { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; } 
}

