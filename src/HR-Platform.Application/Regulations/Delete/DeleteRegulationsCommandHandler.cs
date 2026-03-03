using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Regulations;
using MediatR;

namespace HR_Platform.Application.Regulations.Delete;

internal sealed class DeleteRegulationCommandHandler(
    IRegulationRepository regulationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteRegulationsCommand, ErrorOr<bool>>
{
    private readonly IRegulationRepository _regulationRepository = regulationRepository ?? throw new ArgumentNullException(nameof(regulationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteRegulationsCommand query, CancellationToken cancellationToken)
    {
        if (await _regulationRepository.GetByIdAsync(new RegulationId(query.RegulationId)) is not Regulation regulation)
            return Error.NotFound("Regulation.NotFound", "The Regulation with the provide Id was not found.");

        try
        {
            _regulationRepository.Delete(regulation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}