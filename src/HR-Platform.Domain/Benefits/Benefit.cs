using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Benefits;

public sealed class Benefit : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Benefit()
    {
    }
    //Minute
    public Benefit(BenefitId id, CompanyId companyId, string name, string description, bool isAvailableForAll, int minimumMonthsConstraint, string anotherContraint, bool isAnotherContraint, string fileName, string fileURL, TimeDate creationDateFile, string imageName, string imageURL, bool isAddedButton, string buttonName, bool isSurveyInclude, string emailWhoChangedByTH, string nameWhoChangedByTH, bool isVisible, bool isPinned, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;

        Description = description;

        IsAvailableForAll = isAvailableForAll;
        MinimumMonthsConstraint = minimumMonthsConstraint;
        AnotherContraint = anotherContraint;
        IsAnotherContraint = isAnotherContraint;

        FileName = fileName;
        FileURL = fileURL;
        CreationDateFile = creationDateFile;

        ImageName = imageName;
        ImageURL = imageURL;

        IsAddedButton = isAddedButton;
        ButtonName = buttonName;

        IsSurveyInclude = isSurveyInclude;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsVisible = isVisible;
        IsPinned = isPinned;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public BenefitId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsAvailableForAll {  get; set; }
    public int MinimumMonthsConstraint { get; set; } 
    public bool IsAnotherContraint { get; set; }
    public string AnotherContraint { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
    public string FileURL { get; set; } = string.Empty;
    public TimeDate CreationDateFile { get; set; }

    public string ImageName { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    
    public bool IsAddedButton { get; set; }
    public string ButtonName { get; set; } = string.Empty;

    public bool IsSurveyInclude { get; set; } 

    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;

    public bool IsVisible { get; set; }
    public bool IsPinned { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<CollaboratorBenefitClaim> CollaboratorBenefitClaims { get; set; }

}

