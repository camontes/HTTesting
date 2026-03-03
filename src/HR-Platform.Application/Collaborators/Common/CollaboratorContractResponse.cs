namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorContractResponse
(
    Guid Id,
    Guid AssignationId,
    string AssignationName,
    Guid PositionId,
    string PositionName,
    int AssignationTypeId,
    string AssignationTypeName,
    Guid ContractTypeId,
    string ContractTypeName,
    int CurrencyTypeId,
    string CurrencyTypeName,
    string Salary,
    string Arl,
    string Bonus,
    string TimeWorked, // Time
    string TimeWorkedEnglish, // TimeEnglish
    string EntranceDateFormatSlash, // Time
    string TimeEditedByTh, // Time
    string RoleWhoChangedByTh,
    string NameWhoChangedByTh,
    string EditionDateTimeFormatMonthToltip,
    string EditionDateTimeFormatMonthToltipEnglish

);
