using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.DocumentManagementFileTypes.Common;
using HR_Platform.Domain.DocumentManagementFileTypes;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllDocumentManagementFileTypeQueryHandler(
    IDocumentManagementFileTypeRepository defaultFileTypeRepository
    ) : IRequestHandler<GetAllDocumentManagementFileTypesQuery, ErrorOr<IReadOnlyList<DocumentManagementFileTypesResponse>>>
{
    private readonly IDocumentManagementFileTypeRepository _defaultFileTypeRepository = defaultFileTypeRepository ?? throw new ArgumentNullException(nameof(defaultFileTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<DocumentManagementFileTypesResponse>>> Handle(GetAllDocumentManagementFileTypesQuery query, CancellationToken cancellationToken)
    {
        List<DocumentManagementFileType> documentManagementFileTypes = await _defaultFileTypeRepository.GetAll();

        List<DocumentManagementFileTypesResponse> DocumentManagementFileTypeResponse = [];

        if (documentManagementFileTypes is not null && documentManagementFileTypes.Count > 0)
        {
            foreach (DocumentManagementFileType assignationType in documentManagementFileTypes)
            {
                DocumentManagementFileTypeResponse.Add
                (
                    new DocumentManagementFileTypesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return DocumentManagementFileTypeResponse;
    }
}