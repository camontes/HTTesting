using ErrorOr;
using HR_Platform.Application.StudyAreas.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllStudyAreasQuery() : IRequest<ErrorOr<IReadOnlyList<StudyAreasResponse>>>;