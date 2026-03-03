using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Languages.Common;
using HR_Platform.Domain.DefaultLanguageTypes;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllLanguageNamesQueryHandler(
    IDefaultLanguageTypeRepository defaultLanguageTypeRepository
    ) : IRequestHandler<GetAllLanguagesNamesQuery, ErrorOr<IReadOnlyList<LanguagesResponse>>>
{
    private readonly IDefaultLanguageTypeRepository _defaultLanguageTypeRepository = defaultLanguageTypeRepository ?? throw new ArgumentNullException(nameof(defaultLanguageTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<LanguagesResponse>>> Handle(GetAllLanguagesNamesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultLanguageType> LanguageTypes = await _defaultLanguageTypeRepository.GetAll();

        List<LanguagesResponse> LanguageTypesResponse = [];

        if (LanguageTypes is not null && LanguageTypes.Count > 0)
        {
            foreach (DefaultLanguageType assignationType in LanguageTypes)
            {
                LanguageTypesResponse.Add
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
        
        return LanguageTypesResponse;
    }
}