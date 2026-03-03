using ErrorOr;
using HR_Platform.Domain.Primitives;
using MediatR;
using HR_Platform.Domain.ActiveBreaks;

namespace HR_Platform.Application.ActiveBreaks.UpdateIsVisible;

internal sealed class UpdateActiveBreakIsVisibleCommandHandler
(
    IActiveBreakRepository activeBreakRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<UpdateActiveBreakIsVisibleCommand, ErrorOr<bool>>
{
    private readonly IActiveBreakRepository _activeBreakRepository = activeBreakRepository ?? throw new ArgumentNullException(nameof(activeBreakRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateActiveBreakIsVisibleCommand query, CancellationToken cancellationToken)
    {
        if (await activeBreakRepository.GetByIdAsync(new ActiveBreakId(query.Id)) is not ActiveBreak activeBreak)
        {
            return Error.NotFound("ActiveBreak.NotFound", "The ActiveBreak with the provide Id was not found.");
        }

        activeBreak.IsVisible = !activeBreak.IsVisible;

        _activeBreakRepository.Update(activeBreak);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}