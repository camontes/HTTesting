using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Inductions.Create;

internal sealed class CreateInductionsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IInductionRepository inductionRepository,
    IInductionFileRepository inductionFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateInductionsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly IInductionFileRepository _inductionFileRepository = inductionFileRepository ?? throw new ArgumentNullException(nameof(inductionFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateInductionsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Inductions.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<InductionFile> inductionFiles = [];

        Induction inductionResult = new
        (
            new InductionId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),
            command.Name,
            command.Description,
            command.EmailChangeBy,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
            false, //IsVisible
            creationDate, // IsVisibleChangeDate
            false, // AllowForAllCollaborators
            "",
            false, //IsInductionDeleted
            creationDate, // DeletedDate
            true, // IsEditable
            true, // IsDeleteable
            creationDate, // CreationDate
            creationDate // EditionDate
        );

        _inductionRepository.Add(inductionResult);


        if (command.InductionsList.Count > 0)
        {
            foreach (CreateInductionsObjectCommand item in command.InductionsList)
            {
                InductionFile temp = new
                (
                    new InductionFileId(Guid.NewGuid()),
                    inductionResult.Id,
                    item.FileName,
                    item.UrlFile,
                    true, // IsEditable
                    true, // IsDeleteable
                    creationDate,
                    creationDate
                );
                inductionFiles.Add(temp);
            }
        }

        if (inductionFiles.Count > 0)
        {
            _inductionFileRepository.AddRangeInductionFiles(inductionFiles);
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