using ErrorOr;
using HR_Platform.Application.Benefits.UpdateIsVisible;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Benefits;
using MediatR;
using HR_Platform.Domain.Areas;

namespace HR_Platform.Application.Areas.UpdateIsVisibleForms;

internal sealed class UpdateIsVisibleFormsCommandHandler(
    IAreaRepository areaRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleFormsCommand, ErrorOr<bool>>
{
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleFormsCommand query, CancellationToken cancellationToken)
    {
        if (await _areaRepository.GetByIdAsync(new AreaId(query.Id)) is not Area oldArea)
        {
            return Error.NotFound("Area.NotFound", "The Area with the provide Id was not found.");
        }

        oldArea.IsFormsVisible = !oldArea.IsFormsVisible;
        _areaRepository.Update(oldArea);

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