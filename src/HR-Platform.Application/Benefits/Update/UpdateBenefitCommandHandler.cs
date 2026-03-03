using ErrorOr;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Benefits.Update;

internal sealed class UpdateBenefitsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IBenefitRepository benefitRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateBenefitsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateBenefitsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(editationDateString) is not TimeDate editationDate)
            return Error.Validation("Benefits.CreationDate", "CreationDate is not valid");

        if(!command.IsAvailableForAll && command.MinimumMonthsConstraint == 0 && command.MinimumMonthsConstraint > 999)
            return Error.Validation("Benefits.MinimumMonths", "Minimum Months out of range");

        if (!command.IsAvailableForAll && command.IsAnotherContraint && string.IsNullOrEmpty(command.AnotherContraint))
            return Error.Validation("Benefits.AnotherContraint", "Another Contraint can not be empty");

        if (await _benefitRepository.GetByIdAsync(new BenefitId(command.BenefitId)) is not Benefit oldBenefit)
            return Error.NotFound("Benefit.NotFound", "The Benefit with the provide Id was not found.");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);
            

        if (!string.IsNullOrEmpty(command.Name))
        {
            oldBenefit.Name = command.Name;
        }

        if (!string.IsNullOrEmpty(command.Description))
        {
            oldBenefit.Description = command.Description;
        }

        oldBenefit.IsAvailableForAll = command.IsAvailableForAll;
        oldBenefit.MinimumMonthsConstraint = command.MinimumMonthsConstraint;
        oldBenefit.IsAnotherContraint = command.IsAnotherContraint;
        oldBenefit.AnotherContraint = command.AnotherContraint is not null ? command.AnotherContraint : string.Empty;

        if (command.IsChangedFile)
        {
            oldBenefit.FileURL = command.FileURL;
            oldBenefit.FileName = command.FileName; 
        }

        if (command.IsChangedImage)
        {
            oldBenefit.ImageURL = command.ImageURL;
            oldBenefit.ImageName = command.ImageName;
        }

        oldBenefit.IsAddedButton = command.IsAddedButton;
        oldBenefit.ButtonName = command.ButtonName is not null ? command.ButtonName : string.Empty;
        oldBenefit.IsSurveyInclude = command.IsSurveyInclude;

        oldBenefit.EmailWhoChangedByTH = command.EmailChangeBy;
        oldBenefit.NameWhoChangedByTH = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty;

        oldBenefit.EditionDate = editationDate;

        try
        {
            _benefitRepository.Update(oldBenefit);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}