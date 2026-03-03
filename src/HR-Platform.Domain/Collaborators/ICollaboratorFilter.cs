namespace HR_Platform.Domain.Collaborators;

public interface ICollaboratorFilter 
{
    public bool? IsPendingInvitation { get; set; }
    public CollaboratorId? CollaboratorId{ get; set; }
}
