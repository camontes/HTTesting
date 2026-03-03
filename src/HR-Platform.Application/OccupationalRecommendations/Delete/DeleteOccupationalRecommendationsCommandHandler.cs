using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.OccupationalRecommendations;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.Delete;

internal sealed class DeleteOccupationalRecommendationCommandHandler(
    IOccupationalRecommendationRepository occupationalRecommendationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteOccupationalRecommendationsCommand, ErrorOr<bool>>
{
    private readonly IOccupationalRecommendationRepository _occupationalRecommendationRepository = occupationalRecommendationRepository ?? throw new ArgumentNullException(nameof(occupationalRecommendationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteOccupationalRecommendationsCommand query, CancellationToken cancellationToken)
    {
        if (await _occupationalRecommendationRepository.GetByIdAsync(new OccupationalRecommendationId(query.OccupationalRecommendationId)) is not OccupationalRecommendation occupationalRecommendation)
            return Error.NotFound("OccupationalRecommendation.NotFound", "The OccupationalRecommendation with the provide Id was not found.");

        try
        {
            _occupationalRecommendationRepository.Delete(occupationalRecommendation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}