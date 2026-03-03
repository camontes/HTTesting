using ErrorOr;
using HR_Platform.Application.Assignations.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Assignations;
using MediatR;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateAssignationCommandHandler : IRequestHandler<UpdateAssignationCommand, ErrorOr<AssignationsResponse>>
{
    private readonly IAssignationRepository _assignationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAssignationCommandHandler
    (
        IAssignationRepository assignationRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _assignationRepository = assignationRepository ?? throw new ArgumentNullException(nameof(assignationRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<AssignationsResponse>> Handle(UpdateAssignationCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _assignationRepository.GetByIdAsync(new AssignationId(query.Id)) is not Assignation oldAssignation)
        {
            return Error.NotFound("Assignation.NotFound", "The assignation with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        Assignation assignation = new
        (
            new AssignationId(query.Id),
            new CompanyId(Guid.Parse(query.CompanyId)),
            query.Name,
            query.NameEnglish,
            oldAssignation.IsEditable,
            oldAssignation.IsDeleteable,
            oldAssignation.IsInternalAssignation,
            oldAssignation.CreationDate,
            editionDate
        );

        _assignationRepository.Update(assignation);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        AssignationsResponse assignationResponse = new
        (
            query.Id,
            company.Name,
            0,
            query.Name,
            query.NameEnglish,
            oldAssignation.IsEditable,
            oldAssignation.IsDeleteable,
            oldAssignation.IsInternalAssignation
        );

        return assignationResponse;
    }
}