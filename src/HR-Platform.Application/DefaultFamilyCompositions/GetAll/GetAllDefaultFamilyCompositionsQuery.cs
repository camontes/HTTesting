using ErrorOr;
using HR_Platform.Application.DefaultFamilyCompositions.Common;
using MediatR;

namespace HR_Platform.Application.DefaultFamilyCompositions.GetAll;

public record GetAllDefaultFamilyCompositionsQuery() : IRequest<ErrorOr<List<DefaultFamilyCompositionsResponse>>>;