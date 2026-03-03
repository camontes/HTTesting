using ErrorOr;
using HR_Platform.Application.BenefitClaimAnswers.Common;
using MediatR;

namespace BenefitClaimAnswers.DeleteBenefitFromHistory;

public record DeleteBenefitFromHistoryCommand(Guid CompanyId, string BenefitName, string EmailChangeBy) : IRequest<ErrorOr<List<CollaboratorEmailResponse>>>;