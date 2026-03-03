using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.WorkplaceInformations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceInformations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceInformations.GetByCollaboratorId;

internal sealed class GetWorkplaceInformationsByCollaboratorIdHandler(
    IWorkplaceInformationRepository WorkplaceInformationRepository,
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetWorkplaceInformationsByCollaboratorIdQuery, ErrorOr<WorkplaceInformationsResponse>>
{
    private readonly IWorkplaceInformationRepository _workplaceInformationRepository = WorkplaceInformationRepository ?? throw new ArgumentNullException(nameof(WorkplaceInformationRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<WorkplaceInformationsResponse>> Handle(GetWorkplaceInformationsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        Collaborator? oldCollaborator = null;

        if (!string.IsNullOrEmpty(query.CollaboratorId))
        {
            oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.TryParse(query.CollaboratorId, out Guid result) ? result : Guid.NewGuid()));
        }
        else
        {
            oldCollaborator = await _collaboratorRepository.GetByEmailAsync(query.EmailChangeBy);
        }

        if (oldCollaborator == null)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        List<WorkplaceInformation>? workplaceInformationListFull = await _workplaceInformationRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);
        List<WorkplaceInformation>? workplaceInformationList = workplaceInformationListFull?.Where(h => h.CreationDate.Value.Year.ToString() == query.Year).ToList();
        List<WorkplaceInformationFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (workplaceInformationList is not null && workplaceInformationList.Count > 0)
        {
            foreach (WorkplaceInformation item in workplaceInformationList)
            {
                WorkplaceInformationFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.FileName, // FileName
                   item.UrlFile, // FileURL
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   item.CreationDate.Value, // CreationDate
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                   item.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                   item.NameWhoChangedByTH, // FullNameTh
                   _stringService.GetInitials(item.NameWhoChangedByTH) // ShortNameTh
                );

                filesList.Add(temp);
            }
        }

        if (workplaceInformationListFull is not null)
        {
            distinctYears = workplaceInformationListFull
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        WorkplaceInformationsResponse workplaceInformationsResponse = new
        (
            oldCollaborator.Id.Value,
            oldCollaborator.Document,
            oldCollaborator.DocumentType is not null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.Name,
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            [.. filesList.OrderByDescending(x => x.CreationDate)],
            distinctYears
        );

        return workplaceInformationsResponse;

    }
}