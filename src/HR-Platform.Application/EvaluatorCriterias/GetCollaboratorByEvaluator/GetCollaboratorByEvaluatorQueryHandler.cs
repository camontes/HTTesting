using ErrorOr;
using HR_Platform.Application.EvaluatorCriterias.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByEvaluator;
internal sealed class GetCollaboratorByEvaluatorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorCriteriaRepository collaboratorCriteriaRepository,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetCollaboratorByEvaluatorQuery, ErrorOr<List<CollaboratorByEvalutorResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorCriteriaRepository _collaboratorCriteriaRepository = collaboratorCriteriaRepository ?? throw new ArgumentNullException(nameof(collaboratorCriteriaRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    public async Task<ErrorOr<List<CollaboratorByEvalutorResponse>>> Handle(GetCollaboratorByEvaluatorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.EmailWhoIsLogin) is not Collaborator oldCollaborator)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (!oldCollaborator.IsEvaluator)
            return Error.Validation("Company.IsEvaluator", "The collaborator is not an evaluator.");

        List<CollaboratorCriteria>? collaboratorCriteriaList = await _collaboratorCriteriaRepository.GetByEvaluatorIdAsync(oldCollaborator.Id);
        List<CollaboratorByEvalutorResponse> response = [];

        if (collaboratorCriteriaList is not null && collaboratorCriteriaList.Count > 0)
        {
            var collaboratorCriteriaListWithoutRepeats = collaboratorCriteriaList.DistinctBy(x => x.CollaboratorEvaluatedId.Value).ToList();

            foreach (CollaboratorCriteria item in collaboratorCriteriaListWithoutRepeats)
            {
                CollaboratorByEvalutorResponse temp = new
                (
                    item.Id.Value,
                    item.CollaboratorEvaluated.Id.Value,
                    item.CollaboratorEvaluated.DocumentType is not null ? item.CollaboratorEvaluated.DocumentType.Name : string.Empty,
                    item.CollaboratorEvaluated.DocumentType is not null ? item.CollaboratorEvaluated.OtherDocumentType : string.Empty,
                    item.CollaboratorEvaluated.Document,
                    item.CollaboratorEvaluated.Name,
                    _timeFormatService.GetDateFormatMonthLarge(item.CollaboratorEvaluated.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                    _timeFormatService.GetDateFormatMonthLarge(item.CollaboratorEvaluated.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // EntranceDateEnglish
                    item.CollaboratorEvaluated.Position.Id.Value,
                    item.CollaboratorEvaluated.Position.Name,
                    item.CollaboratorEvaluated.Position.NameEnglish
                );
                response.Add(temp);
            }
        }
        return response;
    }
}