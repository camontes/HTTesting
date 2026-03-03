using ErrorOr;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.BrigadeAdjustments.Delete;

internal sealed class DeleteBrigadeAdjustmentCommandHandler(
    IBrigadeAdjustmentRepository brigadeAdjustmentRepository,
    IBrigadeMemberRepository brigadeMemberRepository,
    ICollaboratorBrigadeRepository collaboratorBrigadeRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBrigadeAdjustmentCommand, ErrorOr<bool>>
{
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly ICollaboratorBrigadeRepository _collaboratorBrigadeRepository = collaboratorBrigadeRepository ?? throw new ArgumentNullException(nameof(collaboratorBrigadeRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteBrigadeAdjustmentCommand command, CancellationToken cancellationToken)
    {
        if (await _brigadeAdjustmentRepository.GetByIdAsync(new BrigadeAdjustmentId(command.BrigadeAdjustmentId)) is not BrigadeAdjustment oldBrigadeAdjustment)
            return Error.NotFound("BrigadeAdjustments.NotFound", "The brigade Adjustment with the provide Id was not found.");

        BrigadeAdjustment? noneBrigadeAdjustment = await _brigadeAdjustmentRepository.GetNoneBrigadeAdjustmentByCompanyIdAsync(new CompanyId(command.CompanyId));

        if (noneBrigadeAdjustment is not null)
        {
            List<BrigadeMember> brigadeMemberList = await _brigadeMemberRepository.GetByBrigadeAdjustmentIdAsync(oldBrigadeAdjustment.Id);
            foreach (BrigadeMember brigadeMember in brigadeMemberList)
            {
                brigadeMember.BrigadeAdjustmentId = noneBrigadeAdjustment.Id;
                brigadeMember.HasBeenDeletedBrigadeAdjustment = true; // This validates the notification on brigade members
            }
            _brigadeMemberRepository.UpdateRange(brigadeMemberList);

            List<CollaboratorBrigade> collaboratorBrigadeList = await _collaboratorBrigadeRepository.GetByBrigadeAdjustmentIdAsync(oldBrigadeAdjustment.Id);
            foreach (CollaboratorBrigade collaboratorBrigade in collaboratorBrigadeList)
            {
                collaboratorBrigade.BrigadeAdjustmentId = noneBrigadeAdjustment.Id;
            }
            _collaboratorBrigadeRepository.UpdateRange(collaboratorBrigadeList);
        }

        _brigadeAdjustmentRepository.Delete(oldBrigadeAdjustment);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}