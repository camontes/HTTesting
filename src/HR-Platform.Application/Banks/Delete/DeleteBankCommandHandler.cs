using ErrorOr;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Banks.Delete;

internal sealed class DeleteBankCommandHandler(
    IBankRepository bankRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBankCommand, ErrorOr<bool>>
{
    private readonly IBankRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteBankCommand query, CancellationToken cancellationToken)
    {
        List<Bank> Banks = await _bankRepository.GetAll();

        if (Banks.Count - query.BankList.Count == 0)
            return Error.Validation("Bank List", "It is not possible to remove all banks, leave at least one");

        List<Bank> listMatched = Banks.Where(x => query.BankList.Any(y => new BankId(y) == x.Id && x.BankAccounts.Count == 0)).ToList();

        if (listMatched.Count == 0)
            return Error.Validation("Bank List", "The Id does not exist");        

        try
        {
            _bankRepository.DeleteRange(listMatched);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}