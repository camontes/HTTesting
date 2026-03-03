using ErrorOr;
using HR_Platform.Application.ProfessionalAdvices.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Update;

internal sealed class UpdateProfessionalAdviceCommandHandler: IRequestHandler<UpdateProfessionalAdviceCommand, ErrorOr<bool>>
{
    private readonly IProfessionalAdviceRepository _ProfessionalAdviceRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateProfessionalAdviceCommandHandler
    (
        IProfessionalAdviceRepository ProfessionalAdviceRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _ProfessionalAdviceRepository = ProfessionalAdviceRepository ?? throw new ArgumentNullException(nameof(ProfessionalAdviceRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateProfessionalAdviceCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _ProfessionalAdviceRepository.GetByIdAsync(new ProfessionalAdviceId(query.Id)) is not ProfessionalAdvice oldProfessionalAdvice)
        {
            return Error.NotFound("ProfessionalAdvice.NotFound", "The Professional Advice with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

         if (!string.IsNullOrEmpty(query.Name) && query.Name != oldProfessionalAdvice.Name)
        {
            oldProfessionalAdvice.Name = query.Name;
            oldProfessionalAdvice.NameEnglish = query.NameEnglish;
            oldProfessionalAdvice.EditionDate = editionDate;
            _ProfessionalAdviceRepository.Update(oldProfessionalAdvice);
        }

        if (!string.IsNullOrEmpty(query.NameAcronyms) && query.Name != oldProfessionalAdvice.NameAcronyms)
        {
            oldProfessionalAdvice.NameAcronyms = query.NameAcronyms.ToUpper();
            oldProfessionalAdvice.NameAcronymsEnglish = query.NameAcronymsEnglish.ToUpper();
            oldProfessionalAdvice.EditionDate = editionDate;
            _ProfessionalAdviceRepository.Update(oldProfessionalAdvice);
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