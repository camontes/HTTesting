using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.DocumentManagements;
using MediatR;

namespace HR_Platform.Application.DocumentManagements.Delete;

internal sealed class DeleteDocumentManagementCommandHandler(
    IDocumentManagementRepository documentManagementRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteDocumentManagementsCommand, ErrorOr<bool>>
{
    private readonly IDocumentManagementRepository _documentManagementRepository = documentManagementRepository ?? throw new ArgumentNullException(nameof(documentManagementRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteDocumentManagementsCommand query, CancellationToken cancellationToken)
    {
        if (await _documentManagementRepository.GetByIdAsync(new DocumentManagementId(query.DocumentManagementId)) is not DocumentManagement documentManagement)
            return Error.NotFound("DocumentManagement.NotFound", "The DocumentManagement with the provide Id was not found.");

        try
        {
            _documentManagementRepository.Delete(documentManagement);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}