using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustmentEntities.Create;

public record CreateBaseBrigadeAdjustmentsCommand(List<BaseBrigadeAdjustmentEntityCommand> BrigadeAdjustmentList) : IRequest<ErrorOr<bool>>;

public record BaseBrigadeAdjustmentEntityCommand(
    string Name,
    int IconId
);

