using ErrorOr;
using HR_Platform.Application.EvaluatorMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.EvaluatorMembers.GetAllMembers;

internal sealed class GetEvaluatorMemberQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorCriteriaRepository collaboratorCriteriaRepository,
    IStringService stringService
    ) : IRequestHandler<GetEvaluatorMemberQuery, ErrorOr<List<EvaluatorMemberResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICollaboratorCriteriaRepository _collaboratorCriteriaRepository = collaboratorCriteriaRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaRepository));

    public async Task<ErrorOr<List<EvaluatorMemberResponse>>> Handle(GetEvaluatorMemberQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<Collaborator> collaboratorsResult = collaborators.Where(x => x.IsEvaluator).ToList();
        List<EvaluatorMemberResponse> response = [];

        foreach (Collaborator collaborator in collaboratorsResult)
        {
            List<CollaboratorCriteria>? colloboratorByEvaluator = await _collaboratorCriteriaRepository.GetByEvaluatorIdAsync(collaborator.Id);

            EvaluatorMemberResponse temp = new
            (
                collaborator.Id.Value,
                collaborator.Name,
                collaborator.Position.Name,
                collaborator.Position.NameEnglish,
                collaborator.Photo,
                _stringService.GetInitials(collaborator.Name),
                colloboratorByEvaluator is not null && colloboratorByEvaluator.Count > 0, //HasCollaboratorsAssined
                colloboratorByEvaluator is not null ? colloboratorByEvaluator
                        .Select(x => new PositionNameResponse(x.Position.Name, x.Position.NameEnglish))
                        .Distinct()
                        .ToList() 
                        : []
            );
            response.Add(temp);
        }
        return response.OrderBy(x => x.Name).ToList();
    }
}