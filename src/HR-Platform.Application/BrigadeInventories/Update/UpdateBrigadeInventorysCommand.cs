using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BrigadeInventories.Update;

public record UpdateBrigadeInventoriesCommand(
    string EmailChangeBy,
    Guid Id,
    string Name,
    string Description,
    string CompanyLocation,
    int Amount,
    Guid UnitMeasureId,
    bool ApplyPurchaseDate,
    bool ApplyExpirationDate,
    string? PurchaseDate,
    string? ExpirationDate,
    string? Observations,
    bool HasChangedFiles,
    List<Guid>? FileNamesDeleted,
    List<UpdateBrigadeInventoriesObjectCommand> BrigadeInventoriesList
) : IRequest<ErrorOr<bool>>;

public record UpdateBrigadeInventoriesObjectCommand(
    IFormFile BrigadeInventoryFile,
    string FileName,
    string UrlFile
);

