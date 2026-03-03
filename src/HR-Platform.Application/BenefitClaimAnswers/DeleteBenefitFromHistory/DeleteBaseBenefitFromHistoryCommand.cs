using ErrorOr;
using HR_Platform.Application.BenefitClaimAnswers.Common;
using MediatR;

namespace BenefitClaimAnswers.DeleteBenefitFromHistory;

public record DeleteBaseBenefitFromHistoryCommand(string BenefitName, string Message) : IRequest<ErrorOr<List<CollaboratorEmailResponse>>>;