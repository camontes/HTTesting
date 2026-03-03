using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Delete;

internal sealed class DeleteProfessionalAdvicesCommandHandler(
    IProfessionalAdviceRepository professionalAdviceRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteProfessionalAdvicesCommand, ErrorOr<bool>>
{
    private readonly IProfessionalAdviceRepository _professionalAdviceRepository = professionalAdviceRepository ?? throw new ArgumentNullException(nameof(professionalAdviceRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteProfessionalAdvicesCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        /* Only 1 Professional Advice validation */

        List<ProfessionalAdvice>? ProfessionalAdvices = await _professionalAdviceRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), 0, 0);

        /* Match ProfessionalAdvice */

        List<ProfessionalAdvice>? tProfessionalAdvicesMatched = ProfessionalAdvices?.Where(x => query.ProfessionalAdvicesList.Any(y => new ProfessionalAdviceId(y) == x.Id && (x.Collaborators == null || x.Collaborators.Count == 0))).ToList();

        try
        {
            if (tProfessionalAdvicesMatched != null && tProfessionalAdvicesMatched.Count > 0)
            {
                _professionalAdviceRepository.DeleteRange(tProfessionalAdvicesMatched);

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