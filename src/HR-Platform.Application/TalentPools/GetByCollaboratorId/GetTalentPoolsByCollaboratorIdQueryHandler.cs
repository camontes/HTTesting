using ErrorOr;
using HR_Platform.Application.TalentPools.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorTalentPools;
using MediatR;

namespace HR_Platform.Application.TalentPools.GetByCollaboratorId;

internal sealed class GetTalentPoolsByCollaboratorIdQueryHandler(
        ICollaboratorTalentPoolRepository collaboratorTalentPoolRepository
    ) : IRequestHandler<GetTalentPoolsByCollaboratorIdQuery, ErrorOr<List<TalentPoolByCollaboratorIdResponse>>>
{
    private readonly ICollaboratorTalentPoolRepository _collaboratorTalentPoolRepository = collaboratorTalentPoolRepository ?? throw new ArgumentNullException(nameof(collaboratorTalentPoolRepository));
    public async Task<ErrorOr<List<TalentPoolByCollaboratorIdResponse>>> Handle(GetTalentPoolsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {

        List<CollaboratorTalentPool>  collaboratorsTalentPools = await _collaboratorTalentPoolRepository.GetByCollaboratorIdAsync(new CollaboratorId(query.CollaboratorId));
        List<TalentPoolByCollaboratorIdResponse> response = [];

        if (collaboratorsTalentPools is not null && collaboratorsTalentPools.Count > 0)
        {
            foreach (CollaboratorTalentPool pool in collaboratorsTalentPools)
            {
                TalentPoolByCollaboratorIdResponse temp = new
                (
                    pool.TalentPool.Id.Value,
                    pool.Id.Value,
                    pool.TalentPool.Tittle
                );
                response.Add( temp );
            }
        }
        return response;
    }
}