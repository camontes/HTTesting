using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.LifePreferences.Common;
using HR_Platform.Domain.DefaultLifePreferences;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllLifePreferenceNamesQueryHandler : IRequestHandler<GetAllLifePreferencesNamesQuery, ErrorOr<IReadOnlyList<LifePreferencesResponse>>>
{
    private readonly IDefaultLifePreferenceRepository _defaultLifePreferenceRepository;

    public GetAllLifePreferenceNamesQueryHandler
    (
        IDefaultLifePreferenceRepository defaultLifePreferenceRepository
    )
    {
        _defaultLifePreferenceRepository = defaultLifePreferenceRepository ?? throw new ArgumentNullException(nameof(defaultLifePreferenceRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<LifePreferencesResponse>>> Handle(GetAllLifePreferencesNamesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultLifePreference> LifePreferences = await _defaultLifePreferenceRepository.GetAll();

        List<LifePreferencesResponse> LifePreferencesResponse = new();

        if (LifePreferences is not null && LifePreferences.Count > 0)
        {
            foreach (DefaultLifePreference assignationType in LifePreferences)
            {
                LifePreferencesResponse.Add
                (
                    new LifePreferencesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return LifePreferencesResponse;
    }
}