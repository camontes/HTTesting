using ErrorOr;
using HR_Platform.Application.DefaultFamilyCompositions.Common;
using HR_Platform.Application.FormAnswerGroupStates.Common;
using HR_Platform.Domain.DefaultFamilyCompositions;
using HR_Platform.Domain.FormAnswerGroupStates;
using MediatR;

namespace HR_Platform.Application.FormAnswerGroupStates.GetAll;

internal sealed class GetAllFormAnswerGroupStatesQueryHandler : IRequestHandler<GetAllFormAnswerGroupStatesQuery, ErrorOr<List<FormAnswerGroupStatesResponse>>>
{
    private readonly IFormAnswerGroupStateRepository _formAnswerGroupStateRepository;

    public GetAllFormAnswerGroupStatesQueryHandler
    (
        IFormAnswerGroupStateRepository formAnswerGroupStateRepository
    )
    {
        _formAnswerGroupStateRepository = formAnswerGroupStateRepository ?? throw new ArgumentNullException(nameof(formAnswerGroupStateRepository));
    }

    public async Task<ErrorOr<List<FormAnswerGroupStatesResponse>>> Handle(GetAllFormAnswerGroupStatesQuery getAllFormAnswerGroupStatesQuery, CancellationToken cancellationToken)
    {
        if (await _formAnswerGroupStateRepository.GetAllWithoutNoneAsync() is not List<FormAnswerGroupState> formAnswerGroupStates)
        {
            return Error.NotFound("FormAnswerGroupStates.NotFound", "The Form Answer Group States was not found.");
        }

        List<FormAnswerGroupStatesResponse> formAnswerGroupStatesResponse = new();

        if (formAnswerGroupStates is not null && formAnswerGroupStates.Count > 0)
        {
            foreach (FormAnswerGroupState formAnswerGroupState in formAnswerGroupStates)
            {
                formAnswerGroupStatesResponse.Add
                (
                    new FormAnswerGroupStatesResponse
                    (
                        formAnswerGroupState.Id.Value,

                        formAnswerGroupState.Name,
                        formAnswerGroupState.NameEnglish
                    )
                );
            }
        }

        return formAnswerGroupStatesResponse;

    }
}