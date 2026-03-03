using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.StudyAreas.Common;
using HR_Platform.Domain.DefaultStudyAreas;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllStudyAreasQueryHandler(
    IDefaultStudyAreaRepository defaultStudyAreaRepository
    ) : IRequestHandler<GetAllStudyAreasQuery, ErrorOr<IReadOnlyList<StudyAreasResponse>>>
{
    private readonly IDefaultStudyAreaRepository _defaultStudyAreaRepository = defaultStudyAreaRepository ?? throw new ArgumentNullException(nameof(defaultStudyAreaRepository));

    public async Task<ErrorOr<IReadOnlyList<StudyAreasResponse>>> Handle(GetAllStudyAreasQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultStudyArea> StudyAreas = await _defaultStudyAreaRepository.GetAll();

        List<StudyAreasResponse> StudyAreasResponse = [];

        if (StudyAreas is not null && StudyAreas.Count > 0)
        {
            foreach (DefaultStudyArea assignationType in StudyAreas)
            {
                StudyAreasResponse.Add
                (
                    new StudyAreasResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return StudyAreasResponse;
    }
}