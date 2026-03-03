using ErrorOr;
using HR_Platform.Application.NewCommunications.UpdateIsVisible;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.NewCommunications.UpdateIsVisibleNewCommunication;

internal sealed class UpdateIsVisibleNewCommunicationCommandHandler(
    INewCommunicationRepository newCommunicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleNewCommunicationCommand, ErrorOr<bool>>
{
    private readonly INewCommunicationRepository _newCommunicationRepository = newCommunicationRepository ?? throw new ArgumentNullException(nameof(newCommunicationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleNewCommunicationCommand query, CancellationToken cancellationToken)
    {
        if (await _newCommunicationRepository.GetByIdAsync(new NewCommunicationId(query.Id)) is not NewCommunication oldNewCommunication)
        {
            return Error.NotFound("NewCommunication.NotFound", "The New Communication with the provide Id was not found.");
        }

        oldNewCommunication.IsVisible = !oldNewCommunication.IsVisible;
        _newCommunicationRepository.Update(oldNewCommunication);

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