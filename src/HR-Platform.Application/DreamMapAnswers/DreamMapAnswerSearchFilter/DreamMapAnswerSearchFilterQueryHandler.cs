using ErrorOr;
using HR_Platform.Application.DreamMapAnswers.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.DreamMapAnswers.DreamMapAnswerSearchFilter;

internal sealed class DreamMapAnswerSearchFilterQueryHandler(
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    ITimeFormatService timeFormatService
    ) : IRequestHandler<DreamMapAnswerSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(DreamMapAnswerSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _collaboratorDreamMapAnswerRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize });

        List<DreamMapAnswersCollaboratorResponse> items = [];

        if (results.TotalCount > 0)
        {
            foreach (CollaboratorDreamMapAnswer item in results.Items)
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
                items.Add(temp);
            }
        }
        return new SearchFilterResponse
        (
            results.TotalCount,
            items,
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}