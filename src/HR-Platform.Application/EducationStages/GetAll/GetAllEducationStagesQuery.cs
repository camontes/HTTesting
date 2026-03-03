using ErrorOr;
using HR_Platform.Application.EducationStages.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllEducationStagesQuery() : IRequest<ErrorOr<IReadOnlyList<EducationStagesResponse>>>;