using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.GetByCompanyId;

public record GetInductionByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<InductionResponse>>>;