using ErrorOr;
using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.BrigadeDocumentations.Delete;

internal sealed class DeleteBrigadeDocumentationCommandHandler(
    IBrigadeDocumentationRepository brigadeDocumentationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBrigadeDocumentationsCommand, ErrorOr<bool>>
{
    private readonly IBrigadeDocumentationRepository _brigadeDocumentationRepository = brigadeDocumentationRepository ?? throw new ArgumentNullException(nameof(brigadeDocumentationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteBrigadeDocumentationsCommand query, CancellationToken cancellationToken)
    {
        if (await _brigadeDocumentationRepository.GetByIdAsync(new BrigadeDocumentationId(query.BrigadeDocumentationId)) is not BrigadeDocumentation brigadeDocumentation)
            return Error.NotFound("BrigadeDocumentation.NotFound", "The Brigade Documentation with the provide Id was not found.");

        try
        {
            _brigadeDocumentationRepository.Delete(brigadeDocumentation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}