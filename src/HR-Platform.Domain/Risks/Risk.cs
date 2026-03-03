using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.RiskTypeMains;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Risks;

public sealed class Risk : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private Risk()
    {
    }

    public Risk(RiskId id, RiskTypeMainId riskTypeMainId, string name, string description, string imageURL, string imageName, TimeDate imageCreationTime, string videoURL, string videoName, bool isVisible, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        RiskTypeMainId = riskTypeMainId;

        Name = name;
        Description = description;
        ImageURL = imageURL;
        ImageName = imageName;
        ImageCreationTime = imageCreationTime;
        VideoURL = videoURL;
        VideoName = videoName;

        IsVisible = isVisible;


        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public RiskId Id { get; set; }

    public RiskTypeMainId RiskTypeMainId { get; set; }
    public RiskTypeMain RiskTypeMain { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageURL { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public TimeDate ImageCreationTime { get; set; }

    public string VideoURL { get; set; } = string.Empty;
    public string VideoName { get; set; } = string.Empty;

    public bool IsVisible { get; set; } = false;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }
}

