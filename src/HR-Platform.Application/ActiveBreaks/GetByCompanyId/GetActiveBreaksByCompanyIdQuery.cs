using ErrorOr;
using HR_Platform.Application.ActiveBreaks.Common;
using MediatR;

namespace HR_Platform.Application.ActiveBreaks.GetByCompanyId;

public record GetActiveBreaksByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<ActiveBreakResponse>>>;