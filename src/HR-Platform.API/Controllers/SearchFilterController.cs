using BenefitClaimAnswers.BenefitClaimAnswerSearchFilter;
using BenefitClaims.BenefitClaimSearchFilter;
using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.BrigadeDocumentations.BrigadeDocumentationSearchFilter;
using HR_Platform.Application.BrigadeInventoriess.BrigadeInventorySearchFilter;
using HR_Platform.Application.CollaboratorBrigadeInventories.CollaboratorBrigadeInventorySearchFilter;
using HR_Platform.Application.Collaborators.CollaboratorSearchFilter;
using HR_Platform.Application.Common;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.DreamMapAnswers.DreamMapAnswerSearchFilter;
using HR_Platform.Application.Inductions.InductionCompletedSearchFilter;
using HR_Platform.Application.Inductions.InductionSentSearchFilter;
using HR_Platform.Application.OccupationalRecommendations.OccupationalRecommendationSearchFilter;
using HR_Platform.Application.Regulations.RegulationSearchFilter;
using HR_Platform.Application.Surveys.SurveysSearchFilter;
using HR_Platform.Application.TalentPools.TalentPoolSearchFilter;
using HR_Platform.Application.WorkplaceEvidences.WorkplaceEvidenceSearchFilter;
using HR_Platform.Application.WorkplaceRecommendations.WorkplaceRecommendationSearchFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using SearchFilters.Query;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("SearchFilter")]
public class SearchFilters(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    public async Task<IActionResult> GetSearchFilter([FromQuery] SearchFilterQuery parameters)
    {
        try
        {
            Token token = new();
            string emailWhoIsLogin = token.GetEmailFromToken(Request);
            GetCompanyByEmailQuery emailQuery = new(emailWhoIsLogin);
            ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

            companyResult.Match(
                company => Ok(company),
                errors => Problem(errors)
            );

            string requestText = parameters.Query.ToLower();

            SearchFilterQueryBase request = parameters.Context switch
            {
                "benefitClaim" => new BenefitClaimSearchFilterQuery(requestText, parameters.Page, parameters.PageSize), //gestionar solicitudes
                "benefitClaimHistory" => new BenefitClaimAnswerSearchFilterQuery(requestText, parameters.Page, parameters.PageSize), //Historico de solicitudes
                "brigadeDocumentation" => new BrigadeDocumentationSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, parameters.Year),
                "brigadeInventory" => new BrigadeInventorySearchFilterQuery(requestText, parameters.Page, parameters.PageSize, parameters.Year), //Inventario general
                "collaborator" => new CollaboratorSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, parameters.IsPendingInvitation), //Collaborators, Examenes ocupacionales, Evaluacion de puesto de trabajo
                "collaboratorBrigadeInventory" => new CollaboratorBrigadeInventorySearchFilterQuery(requestText, parameters.Page, parameters.PageSize, parameters.Year), //Dotación brigadista ** Sale separado
                "dreamMapAnswer" => new DreamMapAnswerSearchFilterQuery(requestText, parameters.Page, parameters.PageSize),
                "inductionsCompleted" => new InductionsCompletedSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, companyResult.Value.Id),
                "inductionsSent" => new InductionsSentSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, companyResult.Value.Id),
                "OccupationalRecommendation" => new OccupationalRecommendationSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, emailWhoIsLogin, parameters.Year), //Mi Salud
                "regulation" => new RegulationSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, parameters.Year), //Normatividad
                "surveys" => new SurveysSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, companyResult.Value.Id, parameters.AreaId),
                "talentPool" => new TalentPoolSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, parameters.IsTalentPoolArchived),
                "workplaceEvidence" => new WorkplaceEvidenceSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, emailWhoIsLogin, parameters.Year), //Mi puesto de trabajo
                "workplaceRecommendation" => new WorkplaceRecommendationSearchFilterQuery(requestText, parameters.Page, parameters.PageSize, emailWhoIsLogin, parameters.CollaboratorId, parameters.Year), //Recomendaciones de puesto de trabajo

                _ => throw new ArgumentException($"Context '{parameters.Context}' is not supported.")
            };

            var response = await _mediator.Send(request);

            return response.Match(
                searchFilter => Ok(searchFilter),
                errors => Problem(errors)
            );
        }
        catch(Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }
}