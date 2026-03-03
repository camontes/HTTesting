using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Create;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Update;

internal sealed class UpdateBrigadeMembersCommandHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    ICollaboratorRepository collaboratorRepository,
    IBrigadeAdjustmentRepository brigadeAdjustmentRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateBrigadeMembersCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateBrigadeMembersCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("BrigadeMembers.EditionDate", "EditionDate is not valid");

        List<BrigadeMember> listPush = [];
        string lastCreationDate = string.Empty;
        List<BrigadeMember> membersList = await _brigadeMemberRepository.GetAll();

        if (membersList.Count != 0)
        {
            lastCreationDate = membersList[0].CreationDate.Value.ToString("MM/dd/yyyy HH:mm:ss");
        }
        _brigadeMemberRepository.DeleteRange(membersList);

        string lastCreationDateValidation = !string.IsNullOrEmpty(lastCreationDate) ? lastCreationDate : editionDateString;

        if (TimeDate.Create(lastCreationDateValidation) is not TimeDate creationDate)
            return Error.Validation("BrigadeMembers.CreationDate", "CreationDate is not valid");

        BrigadeAdjustment? DefaultBrigadeAdjustment = await _brigadeAdjustmentRepository.GetNoneBrigadeAdjustmentByCompanyIdAsync(new CompanyId(command.CompanyId));

        if (command.BrigadeMembersDataList is not null && command.BrigadeMembersDataList.Count > 0)
        {
            foreach (BrigadeMemberData item in command.BrigadeMembersDataList)
            {
                if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(item.CollaboratorId)) is null)
                    return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

                BrigadeMember bm = new
                (
                    new BrigadeMemberId(Guid.NewGuid()),
                    new CollaboratorId(item.CollaboratorId),
                    item.IsMainLeader && DefaultBrigadeAdjustment is not null ? DefaultBrigadeAdjustment.Id : new BrigadeAdjustmentId(Guid.Parse(item.BrigadeAdjustmentId)), //BrigadeId
                    false, //HasBeenDeletedBrigadeAdjustment
                    item.IsMainLeader, //IsMainLeader
                    item.IsBrigadeLeader, //IsBrigadeLeader
                    false, //IsVisible
                    true, //IsEditable
                    true, //IsDeleteable
                    creationDate,
                    editionDate
                );
                listPush.Add(bm);
            }
        }
        try
        {
            _brigadeMemberRepository.AddRange(listPush);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}