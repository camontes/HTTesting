using ErrorOr;
using HR_Platform.Application.StudyTypes.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllStudyTypesQuery() : IRequest<ErrorOr<IReadOnlyList<StudyTypesResponse>>>;