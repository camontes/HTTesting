using ErrorOr;
using HR_Platform.Application.DreamMapQuestions.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DreamMapQuestions;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.GetByCompanyId;

internal sealed class GetDreamMapQuestionsByCompanyIdHandler(
    IDreamMapQuestionRepository dreamMapQuestionRepository,
    ICompanyRepository companyRepository,
    ICalculateTimeDifference calculateTimeDifference,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetDreamMapQuestionsByCompanyIdQuery, ErrorOr<List<DreamMapQuestionsResponse>>>
{
    private readonly IDreamMapQuestionRepository _dreamMapQuestionRepository = dreamMapQuestionRepository ?? throw new ArgumentNullException(nameof(dreamMapQuestionRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<List<DreamMapQuestionsResponse>>> Handle(GetDreamMapQuestionsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (await _dreamMapQuestionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId)) is not List<DreamMapQuestion> dreamMapQuestions)
            return Error.NotFound("DreamMapQuestions.NotFound", "The Talent Pools related with the provide Company Id was not found.");

        List<DreamMapQuestionsResponse> dreamMapQuestionsResponse = [];

        if (dreamMapQuestions is not null && dreamMapQuestions.Count > 0)
        {
            foreach (DreamMapQuestion dreamMapQuestion in dreamMapQuestions)
            {
                dreamMapQuestionsResponse.Add
                (
                    new DreamMapQuestionsResponse
                    (
                        dreamMapQuestion.Id.Value,
                        dreamMapQuestion.Question,
                        dreamMapQuestion.CreationDate.Value
                    )
                );
            }
        }

        return dreamMapQuestionsResponse.OrderBy(x => x.CreationDate).ToList();

    }
}