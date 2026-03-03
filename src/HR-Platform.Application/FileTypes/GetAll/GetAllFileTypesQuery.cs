using ErrorOr;
using HR_Platform.Application.FileTypes.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllFileTypesQuery() : IRequest<ErrorOr<IReadOnlyList<FileTypesResponse>>>;