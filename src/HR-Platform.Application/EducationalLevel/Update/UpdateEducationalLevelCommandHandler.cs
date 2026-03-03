using ErrorOr;
using HR_Platform.Application.EducationalLevels.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Update;

internal sealed class UpdateEducationalLevelCommandHandler: IRequestHandler<UpdateEducationalLevelCommand, ErrorOr<bool>>
{
    private readonly IEducationalLevelRepository _pensionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateEducationalLevelCommandHandler
    (
        IEducationalLevelRepository pensionRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateEducationalLevelCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _pensionRepository.GetByIdAsync(new EducationalLevelId(query.Id)) is not EducationalLevel oldEducationalLevel)
        {
            return Error.NotFound("EducationalLevel.NotFound", "The pension with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

         if (!string.IsNullOrEmpty(query.Name) && query.Name != oldEducationalLevel.Name)
        {
            oldEducationalLevel.Name = query.Name;
            oldEducationalLevel.EditionDate = editionDate;
            _pensionRepository.Update(oldEducationalLevel);
        }

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