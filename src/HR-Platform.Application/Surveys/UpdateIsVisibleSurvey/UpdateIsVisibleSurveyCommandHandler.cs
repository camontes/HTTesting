using ErrorOr;
using HR_Platform.Domain.Primitives;
using MediatR;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Surveys;

namespace HR_Platform.Application.Surveys.UpdateIsVisibleSurvey;

internal sealed class UpdateIsVisibleSurveyCommandHandler
(
    ISurveyRepository surveyRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<UpdateIsVisibleSurveyCommand, ErrorOr<bool>>
{
    private readonly ISurveyRepository _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleSurveyCommand query, CancellationToken cancellationToken)
    {
        if (await surveyRepository.GetByIdWithoutIncludesAsync(new SurveyId(query.Id)) is not Survey oldSurvey)
        {
            return Error.NotFound("Survey.NotFound", "The Survey with the provide Id was not found.");
        }

        try
        {
            oldSurvey.IsVisible = !oldSurvey.IsVisible;
            _surveyRepository.Update(oldSurvey);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}