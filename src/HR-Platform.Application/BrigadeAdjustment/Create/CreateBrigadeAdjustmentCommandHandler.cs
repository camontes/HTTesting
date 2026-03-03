using ErrorOr;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Create;

internal sealed class CreateBrigadeAdjustmentsCommandHandler(IBrigadeAdjustmentRepository brigadeAdjustmentRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBrigadeAdjustmentsCommand, ErrorOr<bool>>
{
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBrigadeAdjustmentsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BrigadeAdjustments.CreationDate", "CreationDate is not valid");

        List<BrigadeAdjustment>? brigadeAdjustmentList = await _brigadeAdjustmentRepository.GetByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));
        List<BrigadeAdjustment> brigadeAdjustmentsToAdd = [];
        int countRepetedIcons = 0;

        if (brigadeAdjustmentList is not null && command.BrigadeAdjustmentsDataList.Count > (3 - brigadeAdjustmentList.Where(x => x.IsDeleteable).ToList().Count))
            return Error.Validation("BrigadeAdjustments.Amount", "Exceeds maximum number of brigades");

        if (command.BrigadeAdjustmentsDataList is not null && command.BrigadeAdjustmentsDataList.Count > 0)
        {
            foreach (BrigadeAdjustmentData brigadeAdjustmentData in command.BrigadeAdjustmentsDataList)
            {
                    BrigadeAdjustment brigadeAdjustment = new(
                    new BrigadeAdjustmentId(Guid.NewGuid()),
                    new CompanyId(Guid.Parse(command.CompanyId)),
                    brigadeAdjustmentData.Name,
                    brigadeAdjustmentData.NameEnglish,
                    brigadeAdjustmentData.IconId,
                    true,
                    true,
                    creationDate,
                    creationDate
                );

                //Validate that the same icons are not added for each brigade.
                if (brigadeAdjustmentsToAdd.Any(x => x.IconId == brigadeAdjustmentData.IconId))
                {
                    countRepetedIcons += 1;
                }
                brigadeAdjustmentsToAdd.Add(brigadeAdjustment);
            }
        }
        //Checks that there is no icon already selected in the database
        if (brigadeAdjustmentList is not null && brigadeAdjustmentList.Any(x => brigadeAdjustmentsToAdd.Any(z => x.IconId == z.IconId)))
        {
            countRepetedIcons += 1;
        }

        try
        {
            if (countRepetedIcons == 0)
            {
                _brigadeAdjustmentRepository.AddRangeBrigadeAdjustments(brigadeAdjustmentsToAdd);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            return Error.Validation("BrigadeAdjustments.IconId", "Select a different icon");
        }
        catch (Exception)
        {
            return false;
        }

    }
}