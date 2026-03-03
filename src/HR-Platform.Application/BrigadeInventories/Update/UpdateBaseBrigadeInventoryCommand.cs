using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BrigadeInventories.Update;

public record UpdateBaseBrigadeInventoryCommand
(
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
    List<IFormFile>? BrigadeInventoryFiles
) : IRequest<ErrorOr<bool>>;


