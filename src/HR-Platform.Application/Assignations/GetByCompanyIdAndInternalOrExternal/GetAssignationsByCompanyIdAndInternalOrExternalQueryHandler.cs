using ErrorOr;
using HR_Platform.Application.Assignations.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Assignations;
using MediatR;

namespace HR_Platform.Application.Assignations.GetByCompanyIdAndInternalOrExternal;

internal sealed class GetAssignationsByCompanyIdAndInternalOrExternalQueryHandler : IRequestHandler<GetAssignationsByCompanyIdAndInternalOrExternalQuery, ErrorOr<List<AssignationsResponse>>>
{
    private readonly IAssignationRepository _assignationRepository;
    private readonly ICompanyRepository _companyRepository;

    public GetAssignationsByCompanyIdAndInternalOrExternalQueryHandler
    (
        IAssignationRepository assignationRepository,
        ICompanyRepository companyRepository
    )
    {
        _assignationRepository = assignationRepository ?? throw new ArgumentNullException(nameof(assignationRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<ErrorOr<List<AssignationsResponse>>> Handle(GetAssignationsByCompanyIdAndInternalOrExternalQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _assignationRepository.GetByCompanyIdAndInternalOrExternalAsync((new CompanyId(query.CompanyId)), query.IsInternalAssignation) is not List<Assignation> assignations)
        {
            return Error.NotFound("Assignation.NotFound", "The job assignationTypes related with the provide Id and assgnation type was not found.");
        }

        List<AssignationsResponse> assignationsResponses = new();

        if (assignations is not null && assignations.Count > 0)
        {
            foreach (Assignation assignation in assignations)
            {
                assignationsResponses.Add
                (
                    new AssignationsResponse
                    (
                        assignation.Id.Value,
                        company.Name,
                        assignation.Collaborators.Count, //NumberCollaborators
                        assignation.Name,
                        assignation.NameEnglish,
                        assignation.IsEditable,
                        assignation.IsDeleteable,
                        assignation.IsInternalAssignation
                    )
                );
            }
        }

        return assignationsResponses;
    }
}