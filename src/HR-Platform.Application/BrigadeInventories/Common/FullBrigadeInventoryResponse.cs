namespace HR_Platform.Application.BrigadeInventories.Common;
public record FullBrigadeInventoryResponse
(
    List<BrigadeInventoryResponse> ListBrigades,
    List<string> Years
);

