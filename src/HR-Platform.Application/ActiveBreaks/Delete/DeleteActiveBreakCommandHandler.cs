using ErrorOr;
using HR_Platform.Domain.ActiveBreaks;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.ActiveBreaks.Delete;

internal sealed class DeleteActiveBreakCommandHandler
(
    IActiveBreakRepository activeBreakRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<DeleteActiveBreakCommand, ErrorOr<bool>>
{
    private readonly IActiveBreakRepository _activeBreakRepository = activeBreakRepository ?? throw new ArgumentNullException(nameof(activeBreakRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteActiveBreakCommand command, CancellationToken cancellationToken)
    {
        if (await _activeBreakRepository.GetByIdAsync(new ActiveBreakId(command.Id)) is not ActiveBreak oldActiveBreak)
            return Error.NotFound("ActiveBreak.NotFound", "The ActiveBreak with the provide Id was not found.");

        try
        {
            _activeBreakRepository.Delete(oldActiveBreak);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}