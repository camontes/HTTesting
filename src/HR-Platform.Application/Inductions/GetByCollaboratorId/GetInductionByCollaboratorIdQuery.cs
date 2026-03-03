using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using MediatR;

namespace HR_Platform.Application.Inductions.GetByCollaboratorId;

public record GetInductionByCollaboratorIdQuery(Guid CompanyId, string CollaboratorEmail) : IRequest<ErrorOr<List<InductionForCollaboratorResponse>>>;