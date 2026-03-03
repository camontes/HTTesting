using ErrorOr;
using HR_Platform.Application.SoftSkills.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllSoftSkillsNamesQuery() : IRequest<ErrorOr<IReadOnlyList<SoftSkillsResponse>>>;