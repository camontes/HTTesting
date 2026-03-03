using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.OccupationalTests;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.OccupationalTests.Delete;

internal sealed class DeleteOccupationalTestCommandHandler(
    IOccupationalTestRepository occupationalTestRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteOccupationalTestsCommand, ErrorOr<bool>>
{
    private readonly IOccupationalTestRepository _occupationalTestRepository = occupationalTestRepository ?? throw new ArgumentNullException(nameof(occupationalTestRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteOccupationalTestsCommand query, CancellationToken cancellationToken)
    {
        if (await _occupationalTestRepository.GetByIdAsync(new OccupationalTestId(query.OccupationalTestId)) is not OccupationalTest occupationalTest)
            return Error.NotFound("OccupationalTest.NotFound", "The OccupationalTest with the provide Id was not found.");

        try
        {
            _occupationalTestRepository.Delete(occupationalTest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}