using ErrorOr;
using HR_Platform.Application.QuestionTypes.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.QuestionTypes;
using MediatR;

namespace HR_Platform.Application.QuestionTypes.GetByCompanyId;

internal sealed class GetQuestionTypesByCompanyIdQueryHandler(
    IQuestionTypeRepository questionTypeRepository
    ) : IRequestHandler<GetQuestionTypesByCompanyIdQuery, ErrorOr<List<QuestionTypesResponse>>>
{
    private readonly IQuestionTypeRepository _questionTypeRepository = questionTypeRepository ?? throw new ArgumentNullException(nameof(questionTypeRepository));

    public async Task<ErrorOr<List<QuestionTypesResponse>>> Handle(GetQuestionTypesByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        List<QuestionType>? questionTypes = await _questionTypeRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));

        //For this sprint(33) we only gonna use the 'Short Text' question type
        if (questionTypes != null)
        {
            questionTypes = questionTypes.Where(q => q.NameEnglish == "Short text").ToList();
        }

        List<QuestionTypesResponse> questionTypesResponse = [];

        if (questionTypes is not null && questionTypes.Count > 0)
        {
            foreach (QuestionType questionType in questionTypes)
            {
                questionTypesResponse.Add
                (
                    new QuestionTypesResponse
                    (
                        questionType.Id.Value,

                        questionType.Name,
                        questionType.NameEnglish,

                        questionType.CreationDate.Value
                    )
                );
            }
        }
        return questionTypesResponse;

    }
}