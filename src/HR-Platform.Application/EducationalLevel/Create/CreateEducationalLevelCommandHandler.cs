using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Create;

internal sealed class CreateEducationalLevelsCommandHandler(IEducationalLevelRepository EducationalLevelRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateEducationalLevelsCommand, ErrorOr<bool>>
{
    private readonly IEducationalLevelRepository _EducationalLevelRepository = EducationalLevelRepository ?? throw new ArgumentNullException(nameof(EducationalLevelRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateEducationalLevelsCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("EducationalLevels.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("EducationalLevels.EditionDate", "EditionDate is not valid");


        List<EducationalLevel> educationalLevelsToAdd = [];

        foreach (EducationalLevelData educationalLevelData in command.EducationalLevelsDataList)
        {
            EducationalLevel educationalLevel = new(
                new EducationalLevelId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(educationalLevelData.CompanyId)),
                educationalLevelData.Name,
                educationalLevelData.NameEnglish,
                educationalLevelData.IsEditable,
                educationalLevelData.IsDeleteable,
                creationDate,
                editionDate
            );
            educationalLevelsToAdd.Add(educationalLevel);
        }

        try
        {
            _EducationalLevelRepository.AddRangeEducationalLevels(educationalLevelsToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}