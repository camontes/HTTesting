using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateFamilyInformation;

public record UpdateFamilyInformationCommand(
    
    string EmailChangeBy,

    Guid Id,

    int MaritalStatusId,

    int FamilyMembersNumber,
    int ChildrenNumber,

    List<UpdateFamilyCompositionCommand> FamilyComposition,
    List<UpdateChildrenCommand> Children

) : IRequest<ErrorOr<UpdateFamilyInformationResponse>>;
