using ErrorOr;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.NewCommunications.Delete;

internal sealed class DeleteNewCommunicationQueryQueryHandler
(
    INewCommunicationRepository newCommunicationRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<DeleteNewCommunicationQuery, ErrorOr<bool>>
{
    private readonly INewCommunicationRepository _newCommunicationRepository = newCommunicationRepository ?? throw new ArgumentNullException(nameof(newCommunicationRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteNewCommunicationQuery query, CancellationToken cancellationToken)
    {
        if (await _newCommunicationRepository.GetByIdAsync(new NewCommunicationId(query.Id)) is not NewCommunication communication)
            return Error.NotFound("Benefit.NotFound", "The Benefit with the provide Id was not found.");

        try
        {
            _newCommunicationRepository.Delete(communication);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}