using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.CurrencyTypes.Common;
using HR_Platform.Domain.DefaultCurrencyTypes;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllCurrencyTypesQueryHandler(
    IDefaultCurrencyTypeRepository defaultCurrencyTypeRepository
    ) : IRequestHandler<GetAllCurrencyTypesQuery, ErrorOr<IReadOnlyList<CurrencyTypesResponse>>>
{
    private readonly IDefaultCurrencyTypeRepository _defaultCurrencyTypeRepository = defaultCurrencyTypeRepository ?? throw new ArgumentNullException(nameof(defaultCurrencyTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<CurrencyTypesResponse>>> Handle(GetAllCurrencyTypesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultCurrencyType> CurrencyTypes = await _defaultCurrencyTypeRepository.GetAll();

        List<CurrencyTypesResponse> CurrencyTypesResponse = [];

        if (CurrencyTypes is not null && CurrencyTypes.Count > 0)
        {
            foreach (DefaultCurrencyType assignationType in CurrencyTypes)
            {
                CurrencyTypesResponse.Add
                (
                    new CurrencyTypesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return CurrencyTypesResponse;
    }
}