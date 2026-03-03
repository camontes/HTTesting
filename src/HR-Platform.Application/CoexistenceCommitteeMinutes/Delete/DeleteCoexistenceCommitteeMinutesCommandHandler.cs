using ErrorOr;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.Delete;

internal sealed class DeleteCoexistenceCommitteeMinuteCommandHandler(
    ICoexistenceCommitteeMinuteRepository coexistenceCommitteeMinuteRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCoexistenceCommitteeMinutesCommand, ErrorOr<bool>>
{
    private readonly ICoexistenceCommitteeMinuteRepository _coexistenceCommitteeMinuteRepository = coexistenceCommitteeMinuteRepository ?? throw new ArgumentNullException(nameof(coexistenceCommitteeMinuteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteCoexistenceCommitteeMinutesCommand query, CancellationToken cancellationToken)
    {
        if (await _coexistenceCommitteeMinuteRepository.GetByIdAsync(new CoexistenceCommitteeMinuteId(query.CoexistenceCommitteeMinuteId)) is not CoexistenceCommitteeMinute coexistenceCommitteeMinute)
            return Error.NotFound("CoexistenceCommitteeMinute.NotFound", "The Coexistence Committee Minute with the provide Id was not found.");

        try
        {
            _coexistenceCommitteeMinuteRepository.Delete(coexistenceCommitteeMinute);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}