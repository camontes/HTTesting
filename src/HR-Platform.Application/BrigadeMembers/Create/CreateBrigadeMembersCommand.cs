using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Create;

public record CreateBrigadeMembersCommand(List<BrigadeMemberData> BrigadeMembersDataList, Guid CompanyId) : IRequest<ErrorOr<bool>>;

public record BrigadeMemberData(
    Guid CollaboratorId,
    string BrigadeAdjustmentId,
    bool IsMainLeader,
    bool IsBrigadeLeader
);

