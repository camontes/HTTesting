using ErrorOr;
using HR_Platform.Application.Forms.Common;
using HR_Platform.Application.Forms.GetByAreaId;
using HR_Platform.Domain.Forms;
using MediatR;

namespace HR_Platform.Application.Forms.GetByFormQuestionsTypeId;

internal sealed class GetQuestionsByFormIdQueryHandler(
    IFormRepository formRepository
    ) : IRequestHandler<GetQuestionsByFormIdQuery, ErrorOr<FormQuestionsResponse>>
{
    private readonly IFormRepository _formRepository = formRepository ?? throw new ArgumentNullException(nameof(formRepository));

    public async Task<ErrorOr<FormQuestionsResponse>> Handle(GetQuestionsByFormIdQuery query, CancellationToken cancellationToken)
    {
        if (await _formRepository.GetByIdAsync(new FormId(query.FormId)) is not Form oldForm)
            return Error.NotFound("Form.NotFound", "The Form with the provide Id was not found.");

        FormQuestionsResponse response = new
        (
            oldForm.Name,
            oldForm.Description,
            oldForm.NoveltyType.Name,
            oldForm.NoveltyType.NameEnglish,
            oldForm.FormQuestionsTypes.Select(x => new QuestionsInForms
            (
                x.Id.Value,
                x.Question,
                x.IsRequired,
                x.QuestionType.Name
            )).ToList()
        );

        return response;
    }
}