using ErrorOr;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.NewCommunications.Update;

internal sealed class UpdateNewCommunicationsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    INewCommunicationRepository newCommunicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateNewCommunicationsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INewCommunicationRepository _newCommunicationRepository = newCommunicationRepository ?? throw new ArgumentNullException(nameof(newCommunicationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateNewCommunicationsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("NewCommunications.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (await _newCommunicationRepository.GetByIdAsync(new NewCommunicationId(command.NewCommunicationId)) is not NewCommunication oldNewCommunication)
            return Error.NotFound("NewCommunication.NotFound", "The New Communication with the provide Id was not found.");


        oldNewCommunication.Name = command.Name;
        oldNewCommunication.Description = command.Description;


        if (command.IsChangedFile)
        {
            oldNewCommunication.FileURL = command.FileURL;
            oldNewCommunication.FileName = command.FileName;
        }

        if (command.IsChangedImage)
        {
            oldNewCommunication.ImageURL = command.ImageURL;
            oldNewCommunication.ImageName = command.ImageName;
        }

        oldNewCommunication.IsSurveyInclude = command.IsSurveyInclude;
        oldNewCommunication.EmailWhoChangedByTH = command.EmailChangeBy;
        oldNewCommunication.NameWhoChangedByTH = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty;

        oldNewCommunication.EditionDate = editionDate;

        try
        {
            _newCommunicationRepository.Update(oldNewCommunication);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}