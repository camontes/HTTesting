using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.TechnologyTools.Common;
using HR_Platform.Domain.DefaultTechnologyNames;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllTechnologyNamesQueryHandler(
    IDefaultTechnologyNameRepository defaultTechnologyNameRepository
    ) : IRequestHandler<GetAllTechnologyNamesQuery, ErrorOr<IReadOnlyList<TechnologyToolsResponse>>>
{
    private readonly IDefaultTechnologyNameRepository _defaultTechnologyNameRepository = defaultTechnologyNameRepository ?? throw new ArgumentNullException(nameof(defaultTechnologyNameRepository));

    public async Task<ErrorOr<IReadOnlyList<TechnologyToolsResponse>>> Handle(GetAllTechnologyNamesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultTechnologyName> TechnologyNames = await _defaultTechnologyNameRepository.GetAll();

        List<TechnologyToolsResponse> TechnologyNamesResponse = [];

        if (TechnologyNames is not null && TechnologyNames.Count > 0)
        {
            foreach (DefaultTechnologyName assignationType in TechnologyNames)
            {
                TechnologyNamesResponse.Add
                (
                    new TechnologyToolsResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return TechnologyNamesResponse;
    }
}