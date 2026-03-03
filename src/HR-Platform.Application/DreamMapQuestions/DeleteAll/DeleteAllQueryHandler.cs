using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DreamMapQuestions;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.DeleteAll;

internal sealed class DeleteAllQueryHandler(
    IDreamMapQuestionRepository dreamMapQuestionRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<DeleteAllQuery, ErrorOr<bool>>
{
    private readonly IDreamMapQuestionRepository _dreamMapQuestionRepository = dreamMapQuestionRepository ?? throw new ArgumentNullException(nameof(dreamMapQuestionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteAllQuery query, CancellationToken cancellationToken)
    {
        if (await _dreamMapQuestionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId)) is not List<DreamMapQuestion> dreamMapQuestions)
            return Error.NotFound("DreamMapQuestions.NotFound", "The Dream Map Questions related with the provide Company Id was not found.");

        if (dreamMapQuestions is not null && dreamMapQuestions.Count > 0)
        {
            _dreamMapQuestionRepository.DeleteRange(dreamMapQuestions);
        }

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