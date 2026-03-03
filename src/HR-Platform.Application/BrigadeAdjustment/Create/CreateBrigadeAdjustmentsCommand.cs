using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Create;

public record CreateBrigadeAdjustmentsCommand(List<BrigadeAdjustmentData> BrigadeAdjustmentsDataList, string CompanyId) : IRequest<ErrorOr<bool>>;

public record BrigadeAdjustmentData(
    string Name,
    string NameEnglish,
    int IconId
);

