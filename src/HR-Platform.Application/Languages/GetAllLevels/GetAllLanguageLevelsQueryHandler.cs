using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Languages.Common;
using HR_Platform.Domain.DefaultLanguageLevels;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllLanguageLevelsQueryHandler : IRequestHandler<GetAllLanguagesLevelsQuery, ErrorOr<IReadOnlyList<LanguagesResponse>>>
{
    private readonly IDefaultLanguageLevelRepository _defaultLanguageLevelRepository;

    public GetAllLanguageLevelsQueryHandler
    (
        IDefaultLanguageLevelRepository defaultLanguageLevelRepository
    )
    {
        _defaultLanguageLevelRepository = defaultLanguageLevelRepository ?? throw new ArgumentNullException(nameof(defaultLanguageLevelRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<LanguagesResponse>>> Handle(GetAllLanguagesLevelsQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultLanguageLevel> LanguageLevels = await _defaultLanguageLevelRepository.GetAll();

        List<LanguagesResponse> LanguageLevelsResponse = new();

        if (LanguageLevels is not null && LanguageLevels.Count > 0)
        {
            foreach (DefaultLanguageLevel assignationType in LanguageLevels)
            {
                LanguageLevelsResponse.Add
                (
                    new LanguagesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return LanguageLevelsResponse;
    }
}