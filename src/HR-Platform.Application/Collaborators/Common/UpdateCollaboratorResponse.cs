using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Application.Collaborators.Common;
public record UpdateCollaboratorResponse
(
    string? EducationalLevel,
    string? ProfessionalAdvice,
    string? ProfessionalCard,
    TimeDate? EditationDate
);
