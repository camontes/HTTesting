using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Create;

internal sealed class CreateAssignationCommandHandler : IRequestHandler<CreateAssignationCommand, ErrorOr<List<Assignation>>>
{
    private readonly IAssignationRepository _assignationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAssignationCommandHandler(IAssignationRepository assignationRepository, IUnitOfWork unitOfWork)
    {
        _assignationRepository = assignationRepository ?? throw new ArgumentNullException(nameof(assignationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<Assignation>>> Handle(CreateAssignationCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        List<Assignation> assignations = new();

        if (command is not null && command.AssignationList is not null && command.AssignationList.Count > 0)
        {
            foreach (CreateAssignationElementCommand element in command.AssignationList)
            {
                if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
                    return Error.Validation("Assignation.CreationDate", "CreationDate is not valid");

                if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
                    return Error.Validation("Assignation.EditionDate", "EditionDate is not valid");

                Assignation assignation = new(new AssignationId(Guid.NewGuid()),
                    new CompanyId(Guid.Parse(element.CompanyId)),
                    element.Name,
                    element.NameEnglish,
                    element.IsEditable,
                    element.IsDeleteable,
                    element.IsInternalAssignation,
                    creationDate,
                    editionDate
                );

                assignations.Add(assignation);

                _assignationRepository.Add(assignation);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        return assignations;
    }
}