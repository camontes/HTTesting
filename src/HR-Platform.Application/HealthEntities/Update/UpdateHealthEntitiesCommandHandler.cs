using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Update;

internal sealed class UpdateHealthEntitiesCommandHandler : IRequestHandler<UpdateHealthEntitiesCommand, ErrorOr<bool>>
{
    private readonly IHealthEntityRepository _healthEntitiesRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHealthEntitiesCommandHandler
    (
        IHealthEntityRepository healthEntitiesRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _healthEntitiesRepository = healthEntitiesRepository ?? throw new ArgumentNullException(nameof(healthEntitiesRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateHealthEntitiesCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _healthEntitiesRepository.GetByIdAsync(new HealthEntityId(query.Id)) is not HealthEntity oldHealthEntities)
        {
            return Error.NotFound("HealthEntities.NotFound", "The health Entities with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");
        
        if (Address.Create
            (
            !string.IsNullOrEmpty(query.StreetAddress) && query.StreetAddress != oldHealthEntities.Address.StreetAddress ? query.StreetAddress : oldHealthEntities.Address.StreetAddress,
            query.CountryCode,
            !string.IsNullOrEmpty(query.Country) && query.Country != oldHealthEntities.Address.Country ? query.Country : oldHealthEntities.Address.Country,
            query.StateCode,
            !string.IsNullOrEmpty(query.State) && query.State != oldHealthEntities.Address.State ? query.State : oldHealthEntities.Address.State,
            query.CityCode,
            !string.IsNullOrEmpty(query.City) && query.City != oldHealthEntities.Address.City ? query.City : oldHealthEntities.Address.City,
            !string.IsNullOrEmpty(query.ZipCode) && query.ZipCode != oldHealthEntities.Address.ZipCode ? query.ZipCode : oldHealthEntities.Address.ZipCode
        ) is not Address address)
            return Error.Validation("HealthEntities.Address", "Address is not valid");

        bool isChange = false;

        if (!string.IsNullOrEmpty(query.Name) && query.Name != oldHealthEntities.Name)
        {
            oldHealthEntities.Name = query.Name;
            oldHealthEntities.NameEnglish = query.NameEnglish;
            isChange = true;
        }

        if(address != oldHealthEntities.Address)
        {
            oldHealthEntities.Address = address;
            isChange = true;
        }

        try
        {
            if (isChange)
            {
                oldHealthEntities.EditionDate = editionDate;
                _healthEntitiesRepository.Update(oldHealthEntities);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }
}