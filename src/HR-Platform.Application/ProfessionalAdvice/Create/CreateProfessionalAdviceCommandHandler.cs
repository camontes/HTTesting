using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ProfessionalAdvices.Create;

internal sealed class CreateProfessionalAdvicesCommandHandler(IProfessionalAdviceRepository ProfessionalAdviceRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProfessionalAdvicesCommand, ErrorOr<bool>>
{
    private readonly IProfessionalAdviceRepository _ProfessionalAdviceRepository = ProfessionalAdviceRepository ?? throw new ArgumentNullException(nameof(ProfessionalAdviceRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateProfessionalAdvicesCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("ProfessionalAdvices.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("ProfessionalAdvices.EditionDate", "EditionDate is not valid");


        List<ProfessionalAdvice> professionalAdvicesToAdd = [];

        foreach (ProfessionalAdviceData professionalAdviceData in command.ProfessionalAdvicesDataList)
        {
            ProfessionalAdvice professionalAdvice = new(
                new ProfessionalAdviceId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(professionalAdviceData.CompanyId)),
                professionalAdviceData.Name,
                professionalAdviceData.NameEnglish,
                professionalAdviceData.NameAcronyms.ToUpper(),
                professionalAdviceData.NameAcronymsEnglish.ToUpper(),
                professionalAdviceData.IsEditable,
                professionalAdviceData.IsDeleteable,
                creationDate,
                editionDate
            );
            professionalAdvicesToAdd.Add(professionalAdvice);
        }

        try
        {
            _ProfessionalAdviceRepository.AddRangeProfessionalAdvices(professionalAdvicesToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}