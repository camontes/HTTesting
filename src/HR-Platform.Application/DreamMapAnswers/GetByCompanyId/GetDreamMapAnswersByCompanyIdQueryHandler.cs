using ErrorOr;
using HR_Platform.Application.DreamMapAnswers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.DreamMapAnswers.GetByCompanyId;

internal sealed class GetDreamMapAnswersByCompanyIdHandler(
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetDreamMapAnswersByCompanyIdQuery, ErrorOr<List<DreamMapAnswersCollaboratorResponse>>>
{
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<List<DreamMapAnswersCollaboratorResponse>>> Handle(GetDreamMapAnswersByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        List<CollaboratorDreamMapAnswer> dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetAllCollaborators();
        List<DreamMapAnswersCollaboratorResponse> listCollaborator = [];

        if (dreamMapAnswers.Count > 0)
        {
            foreach (CollaboratorDreamMapAnswer item in dreamMapAnswers)
            {
                DreamMapAnswersCollaboratorResponse temp = new
                (
                    item.Collaborator.Document,
                    item.Collaborator.DocumentType is not null ? item.Collaborator.DocumentType.Name : string.Empty,
                    item.Collaborator.OtherDocumentType is not null ? item.Collaborator.OtherDocumentType : string.Empty,
                    item.Collaborator.Name,
                    _timeFormatService.GetDateFormatMonthShort(item.Collaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateFormatMonthShort(item.Collaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")),
                    item.Collaborator.Assignation.Name,
                    item.Collaborator.Assignation.NameEnglish,
                    item.Collaborator.Id.Value,
                    item.Collaborator.EntranceDate.Value
                );
                listCollaborator.Add(temp);
            }
        }
        return listCollaborator.OrderByDescending(x => x.EntranceDateTime).ToList();
    }
}