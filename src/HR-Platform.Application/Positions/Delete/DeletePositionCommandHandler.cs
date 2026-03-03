using ErrorOr;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Positions.Delete;

internal sealed class DeletePositionCommandHandler(
    IPositionRepository positionRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeletePositionCommand, ErrorOr<bool>>
{
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeletePositionCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        var listPositions = await _positionRepository.GetDefaultsAsync();

        // Exlcuye los Ids de los default
        var listPositionMatched = query.PositionList.Except(listPositions.Select(obj => obj.Id.Value));

        List<Position> Positions = await _positionRepository.GetAll();
        var listMatched = Positions.Where(x => listPositionMatched.Any(y => new PositionId(y) == x.Id) && x.Collaborators.Count == 0).ToList();

        try
        {
            _positionRepository.DeleteRange(listMatched);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}