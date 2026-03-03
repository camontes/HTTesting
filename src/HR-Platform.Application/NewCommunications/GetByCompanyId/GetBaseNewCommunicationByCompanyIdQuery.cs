using ErrorOr;
using HR_Platform.Application.NewCommunications.Common;
using MediatR;

namespace HR_Platform.Application.NewCommunications.GetByCompanyId;

public record GetBaseNewCommunicationByCompanyIdQuery(bool IsVisible) : IRequest<ErrorOr<List<NewCommunicationFileResponse>>>;