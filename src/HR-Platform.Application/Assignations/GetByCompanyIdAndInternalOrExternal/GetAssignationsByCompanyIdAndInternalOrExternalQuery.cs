using ErrorOr;
using HR_Platform.Application.Assignations.Common;
using MediatR;

namespace HR_Platform.Application.Assignations.GetByCompanyIdAndInternalOrExternal;

public record GetAssignationsByCompanyIdAndInternalOrExternalQuery(Guid CompanyId, bool IsInternalAssignation) : IRequest<ErrorOr<List<AssignationsResponse>>>;