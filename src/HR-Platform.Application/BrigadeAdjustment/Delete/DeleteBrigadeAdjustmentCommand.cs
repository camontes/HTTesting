using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Delete;

public record DeleteBrigadeAdjustmentCommand(Guid CompanyId,Guid BrigadeAdjustmentId) : IRequest<ErrorOr<bool>>;