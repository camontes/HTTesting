using ErrorOr;
using MediatR;
using HR_Platform.Application.Positions.Common;

namespace HR_Platform.Application.Positions.GetByCompanyId;

public record GetPositionsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<PositionsResponse>>>;