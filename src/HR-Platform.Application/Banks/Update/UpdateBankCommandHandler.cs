using ErrorOr;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Banks.Update;

internal sealed class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, ErrorOr<bool>>
{
    private readonly IBankRepository _bankRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBankCommandHandler
    (
        IBankRepository bankRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateBankCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _bankRepository.GetByIdAsync(new BankId(query.Id)) is not Bank oldBank)
        {
            return Error.NotFound("Bank.NotFound", "The bank with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

         if (!string.IsNullOrEmpty(query.Name) && query.Name != oldBank.Name)
        {
            oldBank.Name = query.Name;
            oldBank.EditionDate = editionDate;
            _bankRepository.Update(oldBank);
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