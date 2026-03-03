using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Update;

public record UpdateBrigadeAdjustmentsCommand(Guid BrigadeAdjustmentId, string Name) : IRequest<ErrorOr<bool>>;


