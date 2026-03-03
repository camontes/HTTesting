namespace HR_Platform.Application.BrigadeAdjustments.Common;

public record BrigadeAdjustmentsAndCountResponse(
   List<BrigadeAdjustmentsResponse> BrigadeAdjustmentList,
   bool IsCompletedList
);
