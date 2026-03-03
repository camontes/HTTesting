using ErrorOr;
using HR_Platform.Application.Risks.Common;
using MediatR;

namespace HR_Platform.Application.Risks.GetAllByRiskType;

public record GetAllQuery(Guid CompanyId, bool IsVisible) : IRequest<ErrorOr<List<RiskTypeResponse>>>;