using ErrorOr;
using HR_Platform.Application.Professions.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllProfessionsQuery() : IRequest<ErrorOr<IReadOnlyList<ProfessionsResponse>>>;