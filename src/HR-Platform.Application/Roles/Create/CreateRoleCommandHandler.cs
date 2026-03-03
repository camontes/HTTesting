using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Roles.Create;

internal sealed class CreateRolesCommandHandler : IRequestHandler<CreateRolesCommand, ErrorOr<Guid>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRolesCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateRolesCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Roles.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Roles.EditionDate", "EditionDate is not valid");

        Role role = new(new RoleId(Guid.NewGuid()),
            new CompanyId(Guid.Parse(command.CompanyId)),
            command.Name,
            command.NameEnglish,
            new AreaId(Guid.NewGuid()), // Fix - FK not Works
            command.IsEditable,
            command.IsDeleteable,
            creationDate,
            editionDate
        );

        _roleRepository.Add(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id.Value;
    }
}