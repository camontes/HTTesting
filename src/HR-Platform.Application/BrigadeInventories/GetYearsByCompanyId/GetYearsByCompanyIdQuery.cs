using ErrorOr;
using HR_Platform.Application.BrigadeInventories.Common;
using MediatR;

namespace HR_Platform.Application.BrigadeInventories.GetYearsByCompanyId;

public record GetYearsByCompanyIdQuery(int Language, Guid CompanyId) : IRequest<ErrorOr<BrigadeInventoryYearsListResponse>>;


