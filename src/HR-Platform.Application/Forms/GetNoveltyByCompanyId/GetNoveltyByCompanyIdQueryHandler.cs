using ErrorOr;
using HR_Platform.Application.Forms.Common;
using ErrorOr;
using HR_Platform.Application.Forms.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;
using HR_Platform.Domain.Companies;

namespace HR_Platform.Application.Forms.GetNoveltyByCompanyId;

internal sealed class GetNoveltyByCompanyIdQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IAreaRepository areaRepository,
    ITimeFormatService timeFormatService,
    IFormAnswerRepository formAnswerRepository
    ) : IRequestHandler<GetNoveltyByCompanyIdQuery, ErrorOr<List<NoveltyByCollaboratorResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly IFormAnswerRepository _formAnswerRepository = formAnswerRepository ?? throw new ArgumentNullException(nameof(formAnswerRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<List<NoveltyByCollaboratorResponse>>> Handle(GetNoveltyByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        Collaborator? CollaboratorWhoIsLogin = await _collaboratorRepository.GetByEmailAsync(query.EmailWhoIsLogin);

        if (CollaboratorWhoIsLogin is null)
            return Error.Validation("Form.Validation", "The Collaborator With the provide Email was not found.");

        if (await _areaRepository.GetByIdAsync(new AreaId(query.NoveltyTypeId)) is not Area noveltyType)
            return Error.Validation("Form.NoveltyTypeId", "The Novelty Type with the provide Id was not found.");

        SearchFilter<FormAnswer>? formAnswer
            =
            await _formAnswerRepository.GetByCompanyIdAndNameSearchAsync
            (
                new NoveltiesRequestSearch
                {
                    CompanyId = new CompanyId(query.CompanyId),

                    CollaboratorId = CollaboratorWhoIsLogin.Id,

                    CollaboratorName = query.CollaboratorName,

                    AreaId = noveltyType.Id,

                    Page = query.Page,
                    PageSize = query.PageSize
                }
            );

        List<NoveltyByCollaboratorResponse> response = [];

        if (formAnswer is not null && formAnswer.Items is not null && formAnswer.Items.Count() > 0)
        {
            IEnumerable<IGrouping<string, FormAnswer>>? formAnswersGroup = formAnswer.Items
                .GroupBy(a => a.ReferenceNumber);

            foreach (IGrouping<string, FormAnswer> item in formAnswersGroup)
            {
                Guid idResponse = Guid.NewGuid();
                Guid collaboratorIdResponse = Guid.NewGuid();

                string referenceNumberResponse = string.Empty;
                string noveltyTypeResponse = string.Empty;
                string noveltyTypeEnglishResponse = string.Empty;
                string applicationTypeResponse = string.Empty;
                string collaboratorNameResponse = string.Empty;
                string documentResponse = string.Empty;
                string documentTypeNameResponse = string.Empty;
                string documentTypeNameEnglishResponse = string.Empty;
                string assignationNameResponse = string.Empty;
                string assignationNameEnglishResponse = string.Empty;

                int formAnswerGroupState = 0;
                Guid formAnswerGroup = Guid.Empty;

                DateTime applicationDate = DateTime.MinValue;
                DateTime entranceDate = DateTime.MinValue;

                List<AnswerFormObject> AnswerFormObjectList = [];

                foreach (FormAnswer answer in item)
                {
                    if (
                        string.IsNullOrEmpty(noveltyTypeResponse)
                        &&
                        string.IsNullOrEmpty(noveltyTypeEnglishResponse)
                        &&
                        string.IsNullOrEmpty(applicationTypeResponse)
                        &&
                        string.IsNullOrEmpty(referenceNumberResponse)
                        &&
                        string.IsNullOrEmpty(collaboratorNameResponse)
                        &&
                        string.IsNullOrEmpty(documentResponse)
                        &&
                        string.IsNullOrEmpty(documentTypeNameResponse)
                        &&
                        string.IsNullOrEmpty(documentTypeNameEnglishResponse)
                        &&
                        string.IsNullOrEmpty(assignationNameResponse)
                        &&
                        string.IsNullOrEmpty(assignationNameEnglishResponse)
                        &&
                        applicationDate == DateTime.MinValue
                        &&
                        entranceDate == DateTime.MinValue
                        )
                    {
                        noveltyTypeResponse = answer.FormQuestionsType.Form.NoveltyType.Name;
                        noveltyTypeEnglishResponse = answer.FormQuestionsType.Form.NoveltyType.NameEnglish;
                        applicationTypeResponse = answer.FormQuestionsType.Form.Name;
                        referenceNumberResponse = answer.ReferenceNumber;
                        collaboratorNameResponse = answer.Collaborator.Name;
                        documentResponse = !string.IsNullOrEmpty(answer.Collaborator.Document) ? answer.Collaborator.Document : string.Empty;
                        documentTypeNameResponse = answer.Collaborator.DocumentType is not null && answer.Collaborator.DocumentType.Id.Value != 8
                            ? answer.Collaborator.DocumentType.Name : string.Empty;
                        documentTypeNameEnglishResponse = answer.Collaborator.DocumentType is not null && answer.Collaborator.DocumentType.Id.Value != 8
                            ? answer.Collaborator.DocumentType.NameEnglish : string.Empty;
                        documentTypeNameResponse = answer.Collaborator.DocumentType is not null && answer.Collaborator.DocumentType.Id.Value == 8 &&
                            !string.IsNullOrEmpty(answer.Collaborator.OtherDocumentType)
                            ? answer.Collaborator.OtherDocumentType : documentTypeNameResponse;
                        documentTypeNameEnglishResponse = answer.Collaborator.DocumentType is not null && answer.Collaborator.DocumentType.Id.Value == 8 &&
                            !string.IsNullOrEmpty(answer.Collaborator.OtherDocumentType)
                            ? answer.Collaborator.OtherDocumentType : documentTypeNameEnglishResponse;
                        assignationNameResponse = answer.Collaborator.Assignation.Name;
                        assignationNameEnglishResponse = answer.Collaborator.Assignation.NameEnglish;

                        idResponse = answer.Id.Value;

                        collaboratorIdResponse = answer.Collaborator.Id.Value;

                        applicationDate = answer.CreationDate.Value;

                        entranceDate = answer.Collaborator.EntranceDate.Value;

                        formAnswerGroupState = answer.FormAnswerGroup.FormAnswerGroupStateId.Value;
                        formAnswerGroup = answer.FormAnswerGroupId.Value;
                    }

                    AnswerFormObject answerObject = new
                    (
                        answer.FormQuestionsType.Question,
                        answer.Answer,
                        answer.FormQuestionsType.IsRequired
                    );

                    AnswerFormObjectList.Add(answerObject);
                }

                NoveltyByCollaboratorResponse noveltyBy = new
                (
                    idResponse,

                    collaboratorIdResponse,

                    collaboratorNameResponse,

                    documentResponse.Trim(),
                    documentTypeNameResponse,
                    documentTypeNameEnglishResponse,

                    assignationNameResponse,
                    assignationNameEnglishResponse,

                    referenceNumberResponse,

                    formAnswerGroup,

                    formAnswerGroupState,

                    noveltyTypeResponse,
                    noveltyTypeEnglishResponse,

                    applicationTypeResponse,

                    _timeFormatService.GetDateFormatMonthShort(applicationDate, "dd MMM yyyy", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateFormatMonthShort(applicationDate, "MMM dd yyyy", new CultureInfo("en-US")),

                    _timeFormatService.GetDateTimeFormatMonthToltip(applicationDate, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateTimeFormatMonthToltip(applicationDate, "MMMM dd, yyyy HH:mm tt", new CultureInfo("en-US")),

                    applicationDate,

                    _timeFormatService.GetDateFormatMonthLarge(entranceDate, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                    _timeFormatService.GetDateFormatMonthLarge(entranceDate, "MMMM dd, yyyy", new CultureInfo("en-US")), // EntranceDateEnglish

                    _timeFormatService.GetDateTimeFormatMonthToltip(entranceDate, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateTimeFormatMonthToltip(entranceDate, "MMMM dd, yyyy HH:mm tt", new CultureInfo("en-US")),

                    AnswerFormObjectList
                );

                response.Add(noveltyBy);
            }
        }

        return response.OrderByDescending(x => x.CreationTime).ToList();
    }
}