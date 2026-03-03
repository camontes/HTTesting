using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.EducationStages.Common;
using HR_Platform.Domain.DefaultEducationStages;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllEducationStagesQueryHandler(
    IDefaultEducationStageRepository defaultEducationStageRepository
    ) : IRequestHandler<GetAllEducationStagesQuery, ErrorOr<IReadOnlyList<EducationStagesResponse>>>
{
    private readonly IDefaultEducationStageRepository _defaultEducationStageRepository = defaultEducationStageRepository ?? throw new ArgumentNullException(nameof(defaultEducationStageRepository));

    public async Task<ErrorOr<IReadOnlyList<EducationStagesResponse>>> Handle(GetAllEducationStagesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultEducationStage> EducationStages = await _defaultEducationStageRepository.GetAll();

        List<EducationStagesResponse> EducationStagesResponse = [];

        if (EducationStages is not null && EducationStages.Count > 0)
        {
            foreach (DefaultEducationStage assignationType in EducationStages)
            {
                EducationStagesResponse.Add
                (
                    new EducationStagesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return EducationStagesResponse;
    }
}