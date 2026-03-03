using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Minutes;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Minutes.Delete;

internal sealed class DeleteMinuteCommandHandler(
    IMinuteRepository minuteRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMinutesCommand, ErrorOr<bool>>
{
    private readonly IMinuteRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteMinutesCommand query, CancellationToken cancellationToken)
    {
        if (await _minuteRepository.GetByIdAsync(new MinuteId(query.MinuteId)) is not Minute minute)
            return Error.NotFound("Minute.NotFound", "The Minute with the provide Id was not found.");

        try
        {
            _minuteRepository.Delete(minute);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}