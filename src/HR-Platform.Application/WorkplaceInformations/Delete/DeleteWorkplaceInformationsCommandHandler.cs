using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.WorkplaceInformations;
using MediatR;

namespace HR_Platform.Application.WorkplaceInformations.Delete;

internal sealed class DeleteWorkplaceInformationCommandHandler(
    IWorkplaceInformationRepository workplaceInformationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteWorkplaceInformationsCommand, ErrorOr<bool>>
{
    private readonly IWorkplaceInformationRepository _workplaceInformationRepository = workplaceInformationRepository ?? throw new ArgumentNullException(nameof(workplaceInformationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteWorkplaceInformationsCommand query, CancellationToken cancellationToken)
    {
        if (await _workplaceInformationRepository.GetByIdAsync(new WorkplaceInformationId(query.WorkplaceInformationId)) is not WorkplaceInformation workplaceInformation)
            return Error.NotFound("WorkplaceInformation.NotFound", "The WorkplaceInformation with the provide Id was not found.");

        try
        {
            _workplaceInformationRepository.Delete(workplaceInformation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}