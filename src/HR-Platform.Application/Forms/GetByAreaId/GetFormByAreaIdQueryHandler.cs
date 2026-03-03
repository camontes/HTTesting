using ErrorOr;
using HR_Platform.Application.Forms.Common;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Forms;
using MediatR;

namespace HR_Platform.Application.Forms.GetByAreaId;

internal sealed class GetFormByAreaIdQueryHandler(
    IAreaRepository areaRepository,
    IFormRepository formRepository
    ) : IRequestHandler<GetFormByAreaIdQuery, ErrorOr<List<FormsResponse>>>
{
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly IFormRepository _formRepository = formRepository ?? throw new ArgumentNullException(nameof(formRepository));

    public async Task<ErrorOr<List<FormsResponse>>> Handle(GetFormByAreaIdQuery query, CancellationToken cancellationToken)
    {
        if (await _areaRepository.GetByIdAsync(new AreaId(query.AreaId)) is not Area oldArea)
            return Error.NotFound("Area.NotFound", "The Area with the provide Id was not found.");

        List<Form>? formByAreas = await _formRepository.GetByAreaIdAsync(oldArea.Id);

        List<FormsResponse> response = formByAreas is null 
                ? []
                : formByAreas
                .Select(f => new FormsResponse(
                    f.Id.Value,
                    f.Name,

                    f.IsVisible, // IsVisibleWithEye

                    f.CreationDate.Value
                )).ToList();

        return query.IsOrderingByName ? [.. response.OrderBy(t => t.Name)] : response.OrderByDescending(t => t.CreationTime).ToList();
    }
}