using ErrorOr;
using MediatR;
using HR_Platform.Application.BrigadeAdjustments.Common;

namespace HR_Platform.Application.BrigadeAdjustments.GetByCompanyId;

public record GetBrigadeAdjustmentsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<BrigadeAdjustmentsAndCountResponse>>;