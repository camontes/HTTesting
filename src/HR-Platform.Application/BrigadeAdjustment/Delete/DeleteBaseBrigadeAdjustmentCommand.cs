using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Delete;

public record DeleteBaseBrigadeAdjustmentCommand(Guid BrigadeAdjustmentId) : IRequest<ErrorOr<bool>>;