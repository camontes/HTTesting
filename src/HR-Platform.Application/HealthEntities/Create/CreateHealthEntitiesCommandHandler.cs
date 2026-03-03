using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Create;

internal sealed class CreateHealthEntitiesCommandHandler : IRequestHandler<CreateHealthEntitiesCommand, ErrorOr<bool>>
{
    private readonly IHealthEntityRepository _healthEntityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHealthEntitiesCommandHandler
    (
        IHealthEntityRepository healthEntityRepository,
        IUnitOfWork unitOfWork
    )
    {
        _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));

        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(CreateHealthEntitiesCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("HealthEntities.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("HealthEntities.EditionDate", "EditionDate is not valid");

        List<HealthEntity> healthEntities = new();

        foreach (HealthEntityCommand healthEntityCommand in command.HealthEntitiesList)
        {
            if (Address.Create
            (
                !string.IsNullOrEmpty(healthEntityCommand.StreetAddress) ? healthEntityCommand.StreetAddress : string.Empty,
                healthEntityCommand.CountryCode,
                !string.IsNullOrEmpty(healthEntityCommand.Country) ? healthEntityCommand.Country : string.Empty,
                healthEntityCommand.StateCode,
                !string.IsNullOrEmpty(healthEntityCommand.State) ? healthEntityCommand.State : string.Empty,
                healthEntityCommand.CityCode,
                !string.IsNullOrEmpty(healthEntityCommand.City) ? healthEntityCommand.City : string.Empty,
                !string.IsNullOrEmpty(healthEntityCommand.ZipCode) ? healthEntityCommand.ZipCode : string.Empty
            ) is not Address address)
                return Error.Validation("HealthEntities.Address", "Address is not valid");

            HealthEntity healthEntity = new
            (
                new HealthEntityId(Guid.NewGuid()),

                new CompanyId(Guid.Parse(healthEntityCommand.CompanyId)),

                healthEntityCommand.Name,
                healthEntityCommand.NameEnglish,

                address,

                healthEntityCommand.IsEditable,
                healthEntityCommand.IsDeleteable,

                creationDate,
                editionDate
            );

            healthEntities.Add(healthEntity);
        }

        try
        {
            _healthEntityRepository.AddRange(healthEntities);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}