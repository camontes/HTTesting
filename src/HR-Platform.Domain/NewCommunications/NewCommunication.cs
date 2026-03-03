using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.NewCommunications;

public sealed class NewCommunication : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private NewCommunication()
    {
    }
    
    public NewCommunication(NewCommunicationId id, CompanyId companyId, string name, string description, string fileName, string fileURL, TimeDate creationDateFile, string imageName, string imageURL, bool isSurveyInclude, string emailWhoChangedByTH, string nameWhoChangedByTH, bool isVisible, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;

        Description = description;

        FileName = fileName;
        FileURL = fileURL;
        CreationDateFile = creationDateFile;

        ImageName = imageName;
        ImageURL = imageURL;

        IsSurveyInclude = isSurveyInclude;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsVisible = isVisible;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public NewCommunicationId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
    public string FileURL { get; set; } = string.Empty;
    public TimeDate CreationDateFile { get; set; }

    public string ImageName { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;

    public bool IsSurveyInclude { get; set; } 

    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;

    public bool IsVisible { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

