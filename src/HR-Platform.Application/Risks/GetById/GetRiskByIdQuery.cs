using ErrorOr;
using HR_Platform.Application.Risks.Common;
using MediatR;

namespace HR_Platform.Application.Risks.GetById;

public record GetRiskByIdQuery(Guid Id) : IRequest<ErrorOr<RiskResponse>>;