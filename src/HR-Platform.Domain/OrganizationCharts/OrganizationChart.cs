using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.OrganizationCharts;

public sealed class OrganizationChart : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private OrganizationChart()
    {
    }

    public OrganizationChart(OrganizationChartId id, CompanyId companyId, bool isByFile, bool isByUrl, string fileName, string fileURL, TimeDate fileCreatedDate, string emailWhoChangedByTH, string nameWhoChangedByTH, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        IsByFile = isByFile;
        IsByUrl = isByUrl;
        FileName = fileName;
        FileURL = fileURL;
        FileCreatedDate = fileCreatedDate;

        EmailWhoChangedByTH = emailWhoChangedByTH;
        NameWhoChangedByTH = nameWhoChangedByTH;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public OrganizationChartId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public bool IsByFile { get; set; }
    public bool IsByUrl { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileURL { get; set; } = string.Empty;
    public TimeDate FileCreatedDate { get; set; }

    public string EmailWhoChangedByTH { get; set; } = string.Empty;
    public string NameWhoChangedByTH { get; set; } = string.Empty;

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

