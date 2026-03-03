using ErrorOr;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Positions.Update;

internal sealed class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, ErrorOr<bool>>
{
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePositionCommandHandler
    (
        IPositionRepository positionRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdatePositionCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        if (await _positionRepository.GetByIdAsync(new PositionId(query.Id)) is not Position positionResult)
            return Error.Validation("PositionId", "Position Id not Found");
        
        if (positionResult.Name == "Colaborador")
            return Error.Validation("Position", "Collaborator can not be edited");

        if(!string.IsNullOrWhiteSpace(query.Name) && positionResult.Name != query.Name)
        {
            positionResult.Name = query.Name;
        }

        if (positionResult.Description != query.Description)
        {
            positionResult.Description = query.Description is not null ? query.Description : string.Empty;
        }

        if (query.PositionFile != positionResult.PositionFile)
        {
            positionResult.PositionFile = query.PositionFile is not null ? query.PositionFile : string.Empty;
        }

        if (query.PositionFileName != positionResult.PositionFileName)
        {
            positionResult.PositionFileName = query.PositionFileName is not null ? query.PositionFileName : string.Empty;
        }

        try
        {
            _positionRepository.Update(positionResult);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}