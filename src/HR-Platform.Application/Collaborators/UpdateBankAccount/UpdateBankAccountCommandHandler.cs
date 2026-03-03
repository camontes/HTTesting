using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BankAccounts;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace Collaborators.UpdateBankAccount;

internal sealed class UpdateBankAccountsCommandHandler(IBankAccountRepository BankAccountRepository, IBankRepository BankRepository, ICollaboratorRepository collaboratorRepository, ITypeAccountRepository TypeAccountRepository, IEncryptService EncryptService, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBankAccountsCommand, ErrorOr<bool>>
{
    private readonly IBankAccountRepository _BankAccountRepository = BankAccountRepository ?? throw new ArgumentNullException(nameof(BankAccountRepository));
    private readonly IBankRepository _BankRepository = BankRepository ?? throw new ArgumentNullException(nameof(BankRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ITypeAccountRepository _TypeAccountRepository = TypeAccountRepository ?? throw new ArgumentNullException(nameof(TypeAccountRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IEncryptService _encryptService = EncryptService ?? throw new ArgumentNullException(nameof(EncryptService));

    public async Task<ErrorOr<bool>> Handle(UpdateBankAccountsCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.Parse(command.CollaboratorId))) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("BankAccounts.EditionDate", "EditionDate is not valid");


        if (await _BankRepository.GetByIdAsync(new BankId(Guid.TryParse(command.BankId, out Guid resultBank) ? resultBank : Guid.NewGuid())) is null)
            return Error.Validation("BankId", "Bank Id not Found");

        if (await _TypeAccountRepository.GetByIdAsync(new TypeAccountId(Guid.TryParse(command.TypeAccountId, out Guid resultTypeAccount) ? resultTypeAccount : Guid.NewGuid())) is null)
            return Error.Validation("TypeAccountId", "Type Account Id not Found");

        if (string.IsNullOrEmpty(command.AccountNumber))
            return Error.Validation("accountNumberString", "accountNumberString is not valid or missing");

        if (!Guid.TryParse(command.BankAccountId, out Guid resultBankAccountId))
        {
            return Error.Validation("BankAccountId", "Bank Account Id is not valid or missing");
        }
        // !!Info!!
        //El valor por defecto es el que tiene el numero de cuenta vacío 
        BankAccount? NoneBankAccount = await _BankAccountRepository.GetNoneBankAccountByAccountNumberAsync();
        BankAccountId bankAccountId = new(resultBankAccountId);

        if (bankAccountId == NoneBankAccount?.Id)
        {
            string accountNumberEncrypt = _encryptService.EncryptString(command.AccountNumber);

            BankAccount bankAccountRequest = new(
                new BankAccountId(Guid.NewGuid()),
                new BankId(Guid.Parse(command.BankId)),
                new TypeAccountId(Guid.Parse(command.TypeAccountId)),
                !string.IsNullOrEmpty(accountNumberEncrypt) ? accountNumberEncrypt : string.Empty,
                true,
                true,
                editionDate,
                editionDate
                );
            _BankAccountRepository.Add(bankAccountRequest);
            oldCollaborator.BankAccountId = bankAccountRequest.Id;
        }
        else
        {
            BankAccount? oldBankAccount = await _BankAccountRepository.GetByIdAsync(bankAccountId);
            if (oldBankAccount != null)
            {
                if (new BankId(resultBank) != oldBankAccount.BankId)
                {
                    oldBankAccount.BankId = new BankId(resultBank);
                }

                if (new TypeAccountId(resultTypeAccount) != oldBankAccount.TypeAccountId)
                {
                    oldBankAccount.TypeAccountId = new TypeAccountId(resultTypeAccount);
                }

                string AccountNumberDecrypted = _encryptService.DecryptString(oldBankAccount.AccountNumber);
                if (command.AccountNumber != AccountNumberDecrypted)
                {
                    string accountNumberEncrypted = _encryptService.EncryptString(command.AccountNumber);
                    oldBankAccount.AccountNumber = accountNumberEncrypted is not null ? accountNumberEncrypted : string.Empty;
                }

                _BankAccountRepository.Update(oldBankAccount);

            }
        }

        try
        {
            oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
            oldCollaborator.EmailChangedBy = command.EmailChangeBy;
            oldCollaborator.EditionDate = editionDate;
           
            _collaboratorRepository.Update(oldCollaborator);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}