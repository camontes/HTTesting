using ErrorOr;
using HR_Platform.Application.LifePreferences.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllLifePreferencesNamesQuery() : IRequest<ErrorOr<IReadOnlyList<LifePreferencesResponse>>>;