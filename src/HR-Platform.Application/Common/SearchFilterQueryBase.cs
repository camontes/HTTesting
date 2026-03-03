using ErrorOr;
using HR_Platform.Application.SearchFilters.Common;
using MediatR;

namespace HR_Platform.Application.Common;

public abstract class SearchFilterQueryBase : IRequest<ErrorOr<SearchFilterResponse>> { }