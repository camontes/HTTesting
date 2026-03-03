using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Banks.Create;

internal sealed class CreateBanksCommandHandler(IBankRepository BankRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBanksCommand, ErrorOr<bool>>
{
    private readonly IBankRepository _BankRepository = BankRepository ?? throw new ArgumentNullException(nameof(BankRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBanksCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Banks.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Banks.EditionDate", "EditionDate is not valid");


        List<Bank> banksToAdd = [];

        foreach (BankData bankData in command.BanksDataList)
        {
            Bank bank = new(
                new BankId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(bankData.CompanyId)),
                bankData.Name,
                bankData.NameEnglish,
                bankData.IsEditable,
                bankData.IsDeleteable,
                creationDate,
                editionDate
            );
            banksToAdd.Add(bank);
        }

        try
        {
            _BankRepository.AddRangeBanks(banksToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}