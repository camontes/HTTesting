using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.FileTypes.Common;
using HR_Platform.Domain.DefaultFileTypes;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllFileTypesQueryHandler(
    IDefaultFileTypeRepository defaultFileTypeRepository
    ) : IRequestHandler<GetAllFileTypesQuery, ErrorOr<IReadOnlyList<FileTypesResponse>>>
{
    private readonly IDefaultFileTypeRepository _defaultFileTypeRepository = defaultFileTypeRepository ?? throw new ArgumentNullException(nameof(defaultFileTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<FileTypesResponse>>> Handle(GetAllFileTypesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultFileType> FileTypes = await _defaultFileTypeRepository.GetAll();

        List<FileTypesResponse> FileTypesResponse = [];

        if (FileTypes is not null && FileTypes.Count > 0)
        {
            foreach (DefaultFileType assignationType in FileTypes)
            {
                FileTypesResponse.Add
                (
                    new FileTypesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return FileTypesResponse;
    }
}