using ErrorOr;
using HR_Platform.Application.TypeAccounts.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TypeAccounts;
using MediatR;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Application.TypeAccounts.Update;

internal sealed class UpdateTypeAccountCommandHandler : IRequestHandler<UpdateTypeAccountCommand, ErrorOr<bool>>
{
    private readonly ITypeAccountRepository _typeAccountRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTypeAccountCommandHandler
    (
        ITypeAccountRepository typeAccountRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _typeAccountRepository = typeAccountRepository ?? throw new ArgumentNullException(nameof(typeAccountRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateTypeAccountCommand query, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _typeAccountRepository.GetByIdAsync(new TypeAccountId(query.Id)) is not TypeAccount oldTypeAccount)
        {
            return Error.NotFound("TypeAccount.NotFound", "The typeAccount with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        if (!string.IsNullOrEmpty(query.Name))
        {
            oldTypeAccount.Name = query.Name;
            oldTypeAccount.NameEnglish = query.NameEnglish;
            oldTypeAccount.EditionDate = editionDate;

            _typeAccountRepository.Update(oldTypeAccount);
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