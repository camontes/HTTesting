using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.OccupationalTests.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultFileTypes;
using HR_Platform.Domain.OccupationalTests;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.OccupationalTests.Create;

internal sealed class CreateOccupationalTestsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IOccupationalTestRepository OccupationalTestRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateOccupationalTestsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IOccupationalTestRepository _occupationalTestRepository = OccupationalTestRepository ?? throw new ArgumentNullException(nameof(OccupationalTestRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateOccupationalTestsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("OccupationalTests.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<OccupationalTest> occupationalTestList = [];


        foreach (FileFormatResponse item in command.FormatFiles)
        {
            OccupationalTest result = new
            (
                new OccupationalTestId(Guid.NewGuid()),
                oldCollaborator.Id,
                item.FileName,
                item.FileURL,
                command.EmailChangeBy,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                new DefaultFileTypeId(item.FileTypeId),
                item.Others,
                true,
                true,
                creationDate,
                creationDate
            );
            occupationalTestList.Add(result);
        }
        if (occupationalTestList.Count > 0)
        {
            _occupationalTestRepository.Add(occupationalTestList);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}