using ErrorOr;
using HR_Platform.Application.EducationalLevels.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseEducationalLevelsByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<EducationalLevelsAndCountByCompanyResponse>>>;