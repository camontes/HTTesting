namespace HR_Platform.Application.BrigadeInventories.Common;
public record BrigadeInventoryResponse
(
    Guid IdBrigadeInventory,
    string FullNameTh,
    string TimeUpdate,
    string TimeUpdatedEnglish,
    string TimeUpdatedTolTip,
    string TimeUpdatedTolTipEnglish,

    string Name,
    string Description,
    string CompanyLocation,

    int TotalAmount,
    int EquipmentAmount,
    int AvaliableAmount,
    Guid UnitMeasureId,
    string UnitMeasureName,
    string UnitMeasureNameEnglish,

    string PurchaseDate,
    string PurchaseDateEnglish,
    string ExpirationDate,
    string ExpirationDateEnglish,

    string Observations,

    bool IsDeleted,

    DateTime CreationDate,
    DateTime EditionDate,

   List<BrigadeInventoryFileResponse> FilesList

);

