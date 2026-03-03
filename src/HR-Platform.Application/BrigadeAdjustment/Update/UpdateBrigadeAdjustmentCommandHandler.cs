using ErrorOr;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Update;

internal sealed class UpdateBrigadeAdjustmentsCommandHandler(IBrigadeAdjustmentRepository brigadeAdjustmentRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBrigadeAdjustmentsCommand, ErrorOr<bool>>
{
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateBrigadeAdjustmentsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("BrigadeAdjustments.CreationDate", "CreationDate is not valid");

        if (await _brigadeAdjustmentRepository.GetByIdAsync(new BrigadeAdjustmentId(command.BrigadeAdjustmentId)) is not BrigadeAdjustment oldBrigadeAdjustment)
            return Error.Validation("BrigadeAdjustments.GetById", "The Brigade Adjustment with the provide Id was not found");

        oldBrigadeAdjustment.Name = command.Name;
        oldBrigadeAdjustment.EditionDate = editionDate;

        try
        {
            _brigadeAdjustmentRepository.Update(oldBrigadeAdjustment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}