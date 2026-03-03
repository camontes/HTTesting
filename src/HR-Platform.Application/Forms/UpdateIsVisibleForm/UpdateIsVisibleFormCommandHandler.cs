using ErrorOr;
using HR_Platform.Domain.Primitives;
using MediatR;
using HR_Platform.Domain.Forms;

namespace HR_Platform.Application.Forms.UpdateIsVisibleForm;

internal sealed class UpdateIsVisibleFormsCommandHandler(
    IFormRepository formRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleFormCommand, ErrorOr<bool>>
{
    private readonly IFormRepository _formRepository = formRepository ?? throw new ArgumentNullException(nameof(formRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleFormCommand query, CancellationToken cancellationToken)
    {
        if (await formRepository.GetByIdWithoutIncludesAsync(new FormId(query.Id)) is not Form oldForm)
        {
            return Error.NotFound("Form.NotFound", "The Form with the provide Id was not found.");
        }

        try
        {
            oldForm.IsVisible = !oldForm.IsVisible;
            _formRepository.Update(oldForm);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}