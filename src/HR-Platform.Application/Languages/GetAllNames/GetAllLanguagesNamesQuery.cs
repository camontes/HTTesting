using ErrorOr;
using HR_Platform.Application.Languages.Common;
using HR_Platform.Application.Pensions.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllLanguagesNamesQuery() : IRequest<ErrorOr<IReadOnlyList<LanguagesResponse>>>;