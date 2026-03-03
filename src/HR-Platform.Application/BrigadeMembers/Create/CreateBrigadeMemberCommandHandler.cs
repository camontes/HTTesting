using ErrorOr;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Create;

internal sealed class CreateBrigadeMembersCommandHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    ICollaboratorRepository collaboratorRepository,
    IBrigadeAdjustmentRepository brigadeAdjustmentRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateBrigadeMembersCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBrigadeMembersCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BrigadeMembers.CreationDate", "CreationDate is not valid");

        List<BrigadeMember> listPush = [];

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
                    creationDate
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