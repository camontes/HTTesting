using ErrorOr;
using HR_Platform.Domain.Primitives;
using MediatR;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormAnswerGroupStates;
using HR_Platform.Domain.FormAnswerGroupFiles;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Application.Forms.ChangeNoveltyStateById;

internal sealed class ChangeNoveltyStateByIdCommandHandler
(
    IFormAnswerGroupRepository formAnswerGroupRepository,
    IFormAnswerGroupFileRepository formAnswerGroupFileRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<ChangeNoveltyStateByIdCommand, ErrorOr<bool>>
{
    private readonly IFormAnswerGroupFileRepository _formAnswerGroupFileRepository = formAnswerGroupFileRepository ?? throw new ArgumentNullException(nameof(formAnswerGroupFileRepository));
    private readonly IFormAnswerGroupRepository _formAnswerGroupRepository = formAnswerGroupRepository ?? throw new ArgumentNullException(nameof(formAnswerGroupRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(ChangeNoveltyStateByIdCommand query, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Form.EditionDate", "EditionDate is not valid");

        if (await _formAnswerGroupRepository.GetByIdAsync(new FormAnswerGroupId(query.FormAnswerGroupId)) is not FormAnswerGroup oldformAnswerGroup)
        {
            return Error.NotFound("Form.NotFound", "The Form with the provide Id was not found.");
        }

        try
        {
            oldformAnswerGroup.FormAnswerGroupStateId = new FormAnswerGroupStateId(query.FormAnswerGroupStateId);

            oldformAnswerGroup.DescriptionState = !string.IsNullOrEmpty(query.DescriptionState) ? query.DescriptionState : oldformAnswerGroup.DescriptionState;

            List<FormAnswerGroupFile> formAnswerGroupFiles = [];

            if (query.CreateFormAnswerStateFiles is not null && query.CreateFormAnswerStateFiles.Count > 0)
            {
                foreach (CreateFormAnswerStateFiles fileObject in query.CreateFormAnswerStateFiles)
                {
                    FormAnswerGroupFile tempFile = new
                    (
                        new FormAnswerGroupFileId(Guid.NewGuid()),

                        oldformAnswerGroup.Id,

                        !string.IsNullOrEmpty(fileObject.UrlFile) ? fileObject.UrlFile : string.Empty,
                        !string.IsNullOrEmpty(fileObject.FileName) ? fileObject.FileName : string.Empty,

                        true, //IsEditable
                        true, //IsDeletable

                        editionDate,
                        editionDate
                    );

                    formAnswerGroupFiles.Add(tempFile);
                }
            }

            if (formAnswerGroupFiles is not null && formAnswerGroupFiles.Count > 0)
            {
                foreach (FormAnswerGroupFile file in formAnswerGroupFiles)
                {
                    _formAnswerGroupFileRepository.Update(file);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}