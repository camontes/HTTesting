using ErrorOr;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Inductions.Update;

internal sealed class UpdateInductionsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IInductionRepository inductionRepository,
    IInductionFileRepository inductionFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateInductionsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly IInductionFileRepository _inductionFileRepository = inductionFileRepository ?? throw new ArgumentNullException(nameof(inductionFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateInductionsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Inductions.CreationDate", "CreationDate is not valid");

        if (await _inductionRepository.GetByIdAsync(new InductionId(command.InductionId)) is not Induction oldInduction)
            return Error.NotFound("Induction.NotFound", "The Induction with the provide Id was not found.");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<InductionFile> inductionFiles = [];

        oldInduction.Name = command.Name;
        oldInduction.Description = command.Description;
        oldInduction.EmailWhoChangedByTH = command.EmailChangeBy;
        oldInduction.NameWhoChangedByTH = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : "Personal de TH";
        oldInduction.EditionDate = editionDate;

        _inductionRepository.Update(oldInduction);

        if (command.HasChangedFiles)
        {
            if (command.InductionsList is not null && command.InductionsList.Count > 0)
            {
                foreach (UpdateInductionsObjectCommand item in command.InductionsList)
                {
                    InductionFile temp = new
                    (
                        new InductionFileId(Guid.NewGuid()),
                        oldInduction.Id,
                        item.FileName,
                        item.UrlFile,
                        true, // IsEditable
                        true, // IsDeleteable
                        editionDate,
                        editionDate
                    );
                    inductionFiles.Add(temp);
                }
            }

            if (inductionFiles.Count > 0)
            {
                _inductionFileRepository.AddRangeInductionFiles(inductionFiles);
            }

            List<InductionFile> allFiles = await _inductionFileRepository.GetAll();

            if (command.FileNamesDeleted is not null && command.FileNamesDeleted.Count > 0)
            {
                List<InductionFile> allFilesFiltered = allFiles.Where(x => command.FileNamesDeleted.Contains(x.Id.Value)).ToList();
                _inductionFileRepository.DeleteRange(allFilesFiltered);
            }
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