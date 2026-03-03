using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseCollaboratorsByCompanyIdAndIsPendingInvitationQuery(int IsPendingInvitation, int Page, int PageSize) : IRequest<ErrorOr<List<CollaboratorsResponse>>>;