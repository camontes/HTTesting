using ErrorOr;
using HR_Platform.Application.CollaboratorBrigadeInventories.Common;
using MediatR;

namespace HR_Platform.Application.CollaboratorBrigadeInventories.GetByCompanyId;

public record GetCollaboratorBrigadeInventoryByCompanyIdQuery() : IRequest<ErrorOr<List<BrigadeStaffingResponse>>>;