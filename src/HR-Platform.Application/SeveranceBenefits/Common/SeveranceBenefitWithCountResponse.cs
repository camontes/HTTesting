namespace HR_Platform.Application.SeveranceBenefits.Common;
    public record SeveranceBenefitWithCountResponse
    (
        List<SeveranceBenefitWithCollaboratorCountResponse> SeveranceBenefits, 
        int SeveranceBenefitsCount
    );
