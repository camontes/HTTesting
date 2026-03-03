namespace HR_Platform.Application.CollaboratorBrigadeInventories.Common;
public record CollaboratorBrigadeInventoryResponse
(
     Guid IdBrigadeInventory,
     string NameInventary,
     string Description,
     int DeliveriedAmount,
     string DeliveriedAmountUnit,
     string DeliveryDate,
     string DeliveryDateEnglish,
     string ReturnDate,
     string ReturnDateEnglish,
     string Observations,
     string BrigadeNameBelong,
     bool IsLastBrigade,
     bool IsDeleted,
     List<CollaboratorBrigadeInventoryFileResponse> FilesList,
     DateTime CreationDate
);

