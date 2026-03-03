using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BrigadeInventories.Create;

public record CreateBrigadeInventoriesCommand(
    string EmailChangeBy,
    Guid CompanyId,
    string Name,
    string Description,
    string CompanyLocation,
    int Amount,
    Guid UnitMeasureId,
    string? Other,
    bool ApplyPurchaseDate,
    bool ApplyExpirationDate,
    string? PurchaseDate,
    string? ExpirationDate,
    string? Observations,
    List<CreateBrigadeInventoriesObjectCommand> BrigadeInventoriesList
) : IRequest<ErrorOr<bool>>;

public record CreateBrigadeInventoriesObjectCommand(
    IFormFile BrigadeInventoryFile,
    string FileName,
    string UrlFile
);

