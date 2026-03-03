using ErrorOr;
using HR_Platform.Application.Languages.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllLanguagesLevelsQuery() : IRequest<ErrorOr<IReadOnlyList<LanguagesResponse>>>;