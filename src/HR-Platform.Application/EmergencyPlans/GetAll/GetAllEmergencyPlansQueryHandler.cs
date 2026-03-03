using ErrorOr;
using HR_Platform.Application.EmergencyPlans.Common;
using HR_Platform.Application.EmergencyPlans.GetAllByEmergencyPlanType;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.RiskTypeMains;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.GetAllByEmergencyPlan;

internal sealed class GetAllQueryEmergencyPlansHandler(
    ICompanyRepository companyRepository,
    IRiskTypeMainRepository riskTypeMainRepository,
    IEmergencyPlanTypeRepository emergencyPlanTypeRepository

    ) : IRequestHandler<GetAllEmergencyPlansQuery, ErrorOr<EmergencyPlanResponse>>
{
    private readonly IEmergencyPlanTypeRepository _emergencyPlanTypeRepository = emergencyPlanTypeRepository ?? throw new ArgumentNullException(nameof(emergencyPlanTypeRepository));
    private readonly IRiskTypeMainRepository _riskTypeMainRepository = riskTypeMainRepository ?? throw new ArgumentNullException(nameof(riskTypeMainRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));


    public async Task<ErrorOr<EmergencyPlanResponse>> Handle(GetAllEmergencyPlansQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<EmergencyPlanResponse> emergencyPlanResponses = [];
        List<EmergencyPlanType>? emergencyPlanType = await _emergencyPlanTypeRepository.GetByCompanyIdAsync(oldCompany.Id);


        List<RiskTypeMain>? riskTypeMains = await _riskTypeMainRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<EmergencyPlanObjectRiskResponse> resultRiskOnlyTrue = [];
        List<EmergencyPlanObjectRiskResponse> resultRiskAll = [];
        int contId = 1;
        
        if (riskTypeMains is not null && riskTypeMains.Count != 0)
        {
            foreach (RiskTypeMain item in riskTypeMains)
            {
                EmergencyPlanObjectRiskResponse response = new
                (
                    item.Id.Value.ToString(),
                    item.Name,
                    item.NameEnglish,
                    item.IsVisible,
                    item.CreationDate.Value,
                    contId
                );
                contId++;
                if (item.IsVisible)
                {
                    resultRiskOnlyTrue.Add(response);
                }
                resultRiskAll.Add(response);
            }
        }

        EmergencyPlanResponse responseList = new([], [], [], []);

        static EmergencyPlanObjectResponse ToResponse(EmergencyPlanType item) =>
         new(item.Id.Value.ToString(), item.Name, item.NameEnglish, item.IsVisible, item.CreationDate.Value);

        if (emergencyPlanType is not null && emergencyPlanType.Count != 0)
        {

            var meetingEvacuationPointList = emergencyPlanType
                .Where(item => item.EmergencyPlanMainName == "Punto de encuentro y evacuación")
                .Select(ToResponse)
                .OrderBy(x=> x.CreationTime)
                .ToList();

            var inCaseOfEmergencyList = emergencyPlanType
                .Where(item => item.EmergencyPlanMainName == "En caso de emergencia")
                .Select(ToResponse)
                .OrderBy(x => x.CreationTime)
                .ToList();

            var activitiesList = emergencyPlanType
                .Where(item => item.EmergencyPlanMainName == "Actividades")
                .Select(ToResponse)
                .OrderBy(x => x.CreationTime)
                .ToList();

            responseList = new(
               !query.IsVisible ? [.. resultRiskOnlyTrue.OrderBy(x => x.CreationTime)] : [.. resultRiskAll.OrderBy(x => x.CreationTime)],
               !query.IsVisible ? meetingEvacuationPointList.Where(r => r.IsVisible).ToList() : meetingEvacuationPointList,
               !query.IsVisible ? inCaseOfEmergencyList.Where(r => r.IsVisible).ToList() : inCaseOfEmergencyList,
               !query.IsVisible ? activitiesList.Where(r => r.IsVisible).ToList() : activitiesList
           );
        }

        return responseList;

    }
}