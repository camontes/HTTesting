using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.Companies.ExistsById;

internal sealed class ExistsCompanyByIdQueryHandler : IRequestHandler<ExistsCompanyByIdQuery, ErrorOr<BooleanExistsResponse>>
{
    private readonly ICompanyRepository _companyRepository;

    public ExistsCompanyByIdQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<ErrorOr<BooleanExistsResponse>> Handle(ExistsCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.ExistsAsync(new CompanyId(query.Id)) is false)
            return new BooleanExistsResponse(false);

        return new BooleanExistsResponse(true);
    }
}
