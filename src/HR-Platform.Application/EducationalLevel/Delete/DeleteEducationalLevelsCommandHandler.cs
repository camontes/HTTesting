using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.EducationalLevels.Delete;

internal sealed class DeleteEducationalLevelsCommandHandler(
    IEducationalLevelRepository educationalLevelRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteEducationalLevelsCommand, ErrorOr<bool>>
{
    private readonly IEducationalLevelRepository _educationalLevelRepository = educationalLevelRepository ?? throw new ArgumentNullException(nameof(educationalLevelRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteEducationalLevelsCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        List<EducationalLevel>? educationalLevels = await _educationalLevelRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), 0, 0);

        /* Match Educational Level */

        List<EducationalLevel>? teducationalLevelsMatched = educationalLevels?.Where(x => query.EducationalLevelsList.Any(y => new EducationalLevelId(y) == x.Id && (x.Collaborators == null || x.Collaborators.Count == 0))).ToList();

        try
        {
            if (teducationalLevelsMatched != null && teducationalLevelsMatched.Count > 0)
            {
                _educationalLevelRepository.DeleteRange(teducationalLevelsMatched);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}