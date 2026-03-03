using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.WorkplaceEvidences;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.Delete;

internal sealed class DeleteWorkplaceEvidenceCommandHandler(
    IWorkplaceEvidenceRepository workplaceEvidenceRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteWorkplaceEvidencesCommand, ErrorOr<bool>>
{
    private readonly IWorkplaceEvidenceRepository _workplaceEvidenceRepository = workplaceEvidenceRepository ?? throw new ArgumentNullException(nameof(workplaceEvidenceRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteWorkplaceEvidencesCommand query, CancellationToken cancellationToken)
    {
        if (await _workplaceEvidenceRepository.GetByIdAsync(new WorkplaceEvidenceId(query.WorkplaceEvidenceId)) is not WorkplaceEvidence workplaceEvidence)
            return Error.NotFound("WorkplaceEvidence.NotFound", "The WorkplaceEvidence with the provide Id was not found.");

        try
        {
            _workplaceEvidenceRepository.Delete(workplaceEvidence);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}