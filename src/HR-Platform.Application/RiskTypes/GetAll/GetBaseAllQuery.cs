using ErrorOr;
using HR_Platform.Application.Risks.Common;
using MediatR;

namespace HR_Platform.Application.Risks.GetAllByRiskType;

public record GetBaseAllQuery(bool IsVisible) : IRequest<ErrorOr<List<RiskResponse>>>;