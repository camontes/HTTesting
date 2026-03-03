using ErrorOr;
using MediatR;
using HR_Platform.Application.DocumentTypes.GetAll;
using HR_Platform.Application.DocumentTypes.Common;
using HR_Platform.Domain.DocumentTypes;

namespace HR_Platform.Application.Companies.GetAll;
internal sealed class GetAllDocumentTypesQueryHandler(
    IDocumentTypeRepository documentTypeRepository
    ) : IRequestHandler<GetAllDocumentTypesQuery, ErrorOr<IReadOnlyList<DocumentTypesResponse>>>
{
    private readonly IDocumentTypeRepository _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<DocumentTypesResponse>>> Handle(GetAllDocumentTypesQuery query, CancellationToken cancellationToken)
    {
        IList<DocumentType> documentTypes = await _documentTypeRepository.GetAll();

        List<DocumentTypesResponse> documentTypesResponse = [];

        if (documentTypes is not null && documentTypes.Count > 0)
        {
            foreach (DocumentType documentType in documentTypes)
            {
                documentTypesResponse.Add
                (
                    new DocumentTypesResponse
                    (
                        documentType.Id.Value,

                        documentType.Name,
                        documentType.NameEnglish
                    )
                );
            }
        }

        return documentTypesResponse;
    }
}