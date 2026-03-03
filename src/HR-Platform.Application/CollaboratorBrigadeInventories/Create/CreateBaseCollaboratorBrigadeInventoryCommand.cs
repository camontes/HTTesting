using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.CollaboratorBrigadeInventories.Create;

public record CreateBaseCollaboratorBrigadeInventoryCommand
(
    List<Guid> BrigadeMemberIdList,
    bool SendAllBrigades,
    Guid GeneralBrigadeInventoryId,
    int Amount,
    Guid UnitMeasureId,
    string DeliveryDate,
    string? ReturnDate,
    bool ApplyReturnDate,
    string? Observations,
    List<IFormFile>? BrigadeInventoryFiles

) : IRequest<ErrorOr<bool>>;


