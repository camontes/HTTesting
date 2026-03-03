using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.TechnologyTools.Common;
using HR_Platform.Domain.DefaultKnowledgeLevels;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllKnowledgeLevelsQueryHandler(
    IDefaultKnowledgeLevelRepository defaultKnowledgeLevelRepository
    ) : IRequestHandler<GetAllKnowledgeLevelsQuery, ErrorOr<IReadOnlyList<TechnologyToolsResponse>>>
{
    private readonly IDefaultKnowledgeLevelRepository _defaultKnowledgeLevelRepository = defaultKnowledgeLevelRepository ?? throw new ArgumentNullException(nameof(defaultKnowledgeLevelRepository));

    public async Task<ErrorOr<IReadOnlyList<TechnologyToolsResponse>>> Handle(GetAllKnowledgeLevelsQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultKnowledgeLevel> KnowledgeLevels = await _defaultKnowledgeLevelRepository.GetAll();

        List<TechnologyToolsResponse> knowledgeLevelsResponse = [];

        if (KnowledgeLevels is not null && KnowledgeLevels.Count > 0)
        {
            foreach (DefaultKnowledgeLevel assignationType in KnowledgeLevels)
            {
                knowledgeLevelsResponse.Add
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
        
        return knowledgeLevelsResponse;
    }
}