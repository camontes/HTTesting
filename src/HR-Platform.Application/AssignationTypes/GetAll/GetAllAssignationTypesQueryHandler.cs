using ErrorOr;
using MediatR;
using HR_Platform.Application.AssignationTypes.GetAll;
using HR_Platform.Application.AssignationTypes.Common;
using HR_Platform.Domain.AssignationTypes;

namespace HR_Platform.Application.Companies.GetAll;
internal sealed class GetAllAssignationTypesQueryHandler(
    IAssignationTypeRepository assignationTypeRepository
    ) : IRequestHandler<GetAllAssignationTypesQuery, ErrorOr<IReadOnlyList<AssignationTypesResponse>>>
{
    private readonly IAssignationTypeRepository _assignationTypeRepository = assignationTypeRepository ?? throw new ArgumentNullException(nameof(assignationTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<AssignationTypesResponse>>> Handle(GetAllAssignationTypesQuery query, CancellationToken cancellationToken)
    {
        IList<AssignationType> assignationTypes = await _assignationTypeRepository.GetAll();

        List<AssignationTypesResponse> assignationTypesResponses = [];

        if (assignationTypes is not null && assignationTypes.Count > 0)
        {
            foreach (AssignationType assignationType in assignationTypes)
            {
                assignationTypesResponses.Add
                (
                    new AssignationTypesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }

        return assignationTypesResponses;
    }
}