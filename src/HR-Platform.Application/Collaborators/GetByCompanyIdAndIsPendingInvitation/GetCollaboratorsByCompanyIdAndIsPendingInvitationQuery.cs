using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetCollaboratorsByCompanyIdAndIsPendingInvitationQuery(Guid CompanyId, int IsPendingInvitation, int Page, int PageSize)
    : IRequest<ErrorOr<CollaboratorsAndCountResponse>>;