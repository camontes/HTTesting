using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.SoftSkills.Common;
using HR_Platform.Domain.DefaultSoftSkills;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllSoftSkillNamesQueryHandler(
    IDefaultSoftSkillRepository defaultSoftSkillRepository
    ) : IRequestHandler<GetAllSoftSkillsNamesQuery, ErrorOr<IReadOnlyList<SoftSkillsResponse>>>
{
    private readonly IDefaultSoftSkillRepository _defaultSoftSkillRepository = defaultSoftSkillRepository ?? throw new ArgumentNullException(nameof(defaultSoftSkillRepository));

    public async Task<ErrorOr<IReadOnlyList<SoftSkillsResponse>>> Handle(GetAllSoftSkillsNamesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultSoftSkill> SoftSkills = await _defaultSoftSkillRepository.GetAll();

        List<SoftSkillsResponse> SoftSkillsResponse = [];

        if (SoftSkills is not null && SoftSkills.Count > 0)
        {
            foreach (DefaultSoftSkill assignationType in SoftSkills)
            {
                SoftSkillsResponse.Add
                (
                    new SoftSkillsResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return SoftSkillsResponse;
    }
}