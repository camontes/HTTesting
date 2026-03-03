using ErrorOr;
using HR_Platform.Application.BrigadeInventories.Common;
using MediatR;

namespace HR_Platform.Application.BrigadeInventories.GetByCompanyId;

public record GetBrigadeInventoryByCompanyIdQuery(Guid CompanyId, string Year) : IRequest<ErrorOr<FullBrigadeInventoryResponse>>;