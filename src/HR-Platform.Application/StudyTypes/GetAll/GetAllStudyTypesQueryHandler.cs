using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.StudyTypes.Common;
using HR_Platform.Domain.DefaultStudyTypes;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllStudyTypesQueryHandler(
    IDefaultStudyTypeRepository defaultStudyTypeRepository
    ) : IRequestHandler<GetAllStudyTypesQuery, ErrorOr<IReadOnlyList<StudyTypesResponse>>>
{
    private readonly IDefaultStudyTypeRepository _defaultStudyTypeRepository = defaultStudyTypeRepository ?? throw new ArgumentNullException(nameof(defaultStudyTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<StudyTypesResponse>>> Handle(GetAllStudyTypesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultStudyType> StudyTypes = await _defaultStudyTypeRepository.GetAll();

        List<StudyTypesResponse> StudyTypesResponse = [];

        if (StudyTypes is not null && StudyTypes.Count > 0)
        {
            foreach (DefaultStudyType assignationType in StudyTypes)
            {
                StudyTypesResponse.Add
                (
                    new StudyTypesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return StudyTypesResponse;
    }
}