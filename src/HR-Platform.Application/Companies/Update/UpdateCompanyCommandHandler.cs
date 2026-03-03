using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Companies.Update;

internal sealed class UpdateCompanyCommandHandler(
    ICompanyRepository companyRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCompanyCommand, ErrorOr<Guid>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<Guid>> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        /* Creation and edition dates */

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        /* Company validations */

        if (Email.Create(command.Email) is not Email email)
            return Error.Validation("Companies.Email", "Email has not valid format");

        if (Email.Create(command.RequestsEmail!) is not Email requestsEmail)
            return Error.Validation("Companies.RequestsEmail", "RequestsEmail has not valid format");

        if (Address.Create(command.StreetAddress!, command.CountryCode, command.Country!,
            command.StateCode, command.State!, command.CityCode, command.City!, command.ZipCode!) is not Address address)
            return Error.Validation("Companies.Address", "Address is not valid");

        if (PhoneNumber.Create(command.PhoneNumber!) is not PhoneNumber phoneNumber)
            return Error.Validation("Companies.PhoneNumber", "PhoneNumber has not valid format");

        if (TimeDate.Create(command.CreationDate) is not TimeDate creationDate)
            return Error.Validation("Companies.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Companies.EditionDate", "EditionDate is not valid");

        /* Company mapping */

        Company company = new
        (
            new(Guid.Parse(command.Id)),

            email,
            requestsEmail,

            command.CompanyName,
            !string.IsNullOrEmpty(command.MenuName) ? command.MenuName : string.Empty,

            address,

            phoneNumber,

            !string.IsNullOrEmpty(command.LogoURL) ? command.LogoURL : string.Empty,
            !string.IsNullOrEmpty(command.LogoName) ? command.LogoName : string.Empty,

            !string.IsNullOrEmpty(command.URL) ? command.URL : string.Empty,

            creationDate,
            editionDate
        );       

        _companyRepository.Update(company);

        /* Save changes */

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        /* Return Company */

        return company.Id.Value;
    }
}