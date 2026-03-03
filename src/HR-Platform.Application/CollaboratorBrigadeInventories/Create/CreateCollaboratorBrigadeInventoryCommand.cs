using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.CollaboratorBrigadeInventories.Create;

public record CreateCollaboratorBrigadeInventoryCommand(
    string EmailChangeBy,
    Guid CompanyId,
    List<Guid> BrigadeMemberIdList,
    bool SendAllBrigades,
    Guid GeneralBrigadeInventoryId,
    int Amount,
    Guid UnitMeasureId,
    string DeliveryDate,
    bool ApplyReturnDate,
    string? ReturnDate,
    string? Observations,
    List<CollaboratorBrigadeInventoryObject> BrigadeInventoriesFileList
) : IRequest<ErrorOr<bool>>;

public record CollaboratorBrigadeInventoryObject(
    IFormFile BrigadeInventoryFile,
    string FileName,
    string UrlFile
);

