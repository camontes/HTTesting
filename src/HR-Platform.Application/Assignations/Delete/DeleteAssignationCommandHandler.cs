using ErrorOr;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HR_Platform.Application.Assignations.Delete;

internal sealed class DeleteAssignationCommandHandler : IRequestHandler<DeleteAssignationCommand, ErrorOr<bool>>
{
    private readonly IAssignationRepository _assignationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAssignationCommandHandler
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

    public async Task<ErrorOr<bool>> Handle(DeleteAssignationCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        var listAssinations = await _assignationRepository.GetDefaultsAsync();
        // Exlcuye los Ids de los default
        var listAssinationMatched =query.AssignationList.Except(listAssinations.Select(obj => obj.Id.Value));

        List<Assignation> assignations = await _assignationRepository.GetAll();
        var listMatched = assignations.Where(x => listAssinationMatched.Any(y => new AssignationId(y) == x.Id) && x.Collaborators.Count == 0).ToList();

        try
        {
            _assignationRepository.DeleteRange(listMatched);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}