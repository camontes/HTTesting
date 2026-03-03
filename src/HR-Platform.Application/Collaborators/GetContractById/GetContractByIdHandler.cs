using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.GetContractById;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Contracts;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.GetById;

internal sealed class GetContractByIdHandler : IRequestHandler<GetContractByIdQuery, ErrorOr<CollaboratorContractResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly ICollaboratorContractRepository _collaboratorContractRepository;
    private readonly ICalculateTimeCollaboratorWorked _calculateTimeCollaboratorWorked;
    private readonly ITimeFormatService _timeFormatService;


    public GetContractByIdHandler
    (
        ICollaboratorRepository collaboratorRepository,
        ICalculateTimeCollaboratorWorked calculateTimeCollaboratorWorked,
        ICollaboratorContractRepository collaboratorContractRepository,
        ITimeFormatService timeFormatService
    )
    {
        _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
        _collaboratorContractRepository = collaboratorContractRepository ?? throw new ArgumentNullException(nameof(collaboratorContractRepository));
        _calculateTimeCollaboratorWorked = calculateTimeCollaboratorWorked ?? throw new ArgumentNullException(nameof(calculateTimeCollaboratorWorked));
        _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    }

    public async Task<ErrorOr<CollaboratorContractResponse>> Handle(GetContractByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(query.Id) is not Collaborator collaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if(await _collaboratorContractRepository.GetByIdAsync(collaborator.CollaboratorContractId) is not CollaboratorContract contractResult)
            return Error.NotFound("Contract.NotFound", "The Collaborator Contract Id was not found.");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(contractResult.EmailWhoChangedByTH);

        return new CollaboratorContractResponse
        (
            contractResult.Id.Value,
            collaborator.AssignationId.Value,
            collaborator.Assignation.Name,
            collaborator.PositionId.Value,
            collaborator.Position.Name,
            collaborator.AssignationTypeId.Value,
            collaborator.AssignationType.Name,
            contractResult.ContractTypeId.Value,
            contractResult.ContractTypes.Name,
            contractResult.DefaultCurrencyTypeId.Value,
            contractResult.DefaultCurrencyTypes.Name,
            contractResult.Salary,
            contractResult.Arl,
            contractResult.Bonus,
            String.Join(" ", _calculateTimeCollaboratorWorked.CalculateTimeCollaboratorWorkedFunction(collaborator.EntranceDate.Value).Split('.')[0]), // TimeWorked
            String.Join(" ", _calculateTimeCollaboratorWorked.CalculateTimeCollaboratorWorkedFunction(collaborator.EntranceDate.Value).Split('.')[1]), // TimeWorkedEnglish
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthLarge(contractResult.EditionDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : "Equipo de talento humano",
            contractResult.NameWhoChangedByTH,
            _timeFormatService.GetDateTimeFormatMonthToltip(contractResult.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
            _timeFormatService.GetDateTimeFormatMonthToltip(contractResult.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))

        );
    }
}