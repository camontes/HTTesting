using ErrorOr;
using HR_Platform.Application.OccupationalTests.Common;
using HR_Platform.Application.OccupationalTests.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.OccupationalTests.GetByCollaboratorId;

internal sealed class GetOccupationalTestsAllCollaboratorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService
    ) : IRequestHandler<GetOccupationalTestsAllCollaboratorQuery, ErrorOr<AllCollaboratorsResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<AllCollaboratorsResponse>> Handle(GetOccupationalTestsAllCollaboratorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetAllByFilter(query.Page, query.PageSize) is not List<Collaborator> oldCollaboratorList)
            return Error.Validation("Get All Collaborator Error");

        List<Collaborator> tempList = oldCollaboratorList.Where(c => c.BusinessEmail.Value != "superadminth@exsis.com.co").ToList();

        List<DataCollaborator> listAllCollaborator = [];

        if (tempList is not null && tempList.Count > 1)
        {
            foreach (Collaborator item in tempList)
            {
                DataCollaborator temp = new
                (
                    item.Id.Value, // CollaboratorId
                    item.Document, // Document
                    item.DocumentType is not null ? item.DocumentType.Name : string.Empty, // DocumentType
                    item.OtherDocumentType is not null ? item.OtherDocumentType : string.Empty, // OtherDocumentType
                    item.Name, // Name
                    _timeFormatService.GetDateFormatMonthShort(item.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")), // IntranceDate
                    item.Assignation.Name, // Assignation
                    item.EntranceDate.Value
                );
                listAllCollaborator.Add(temp);
            }
        }
        AllCollaboratorsResponse resultAll = new (
           [.. listAllCollaborator.OrderByDescending(x => x.EntranceDate)],
            listAllCollaborator.Count
        );

        return resultAll;
    }
}