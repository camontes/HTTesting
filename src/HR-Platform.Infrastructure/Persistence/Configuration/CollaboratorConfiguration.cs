using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorConfiguration : IEntityTypeConfiguration<Collaborator>
{
    public void Configure(EntityTypeBuilder<Collaborator> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            collaboratorId => collaboratorId.Value,
            value => new CollaboratorId(value)
        );


        builder.HasOne(c => c.AssignationType).WithMany(a => a.Collaborators).HasForeignKey(c => c.AssignationTypeId);

        builder.HasOne(c => c.Assignation).WithMany(a => a.Collaborators).HasForeignKey(c => c.AssignationId);

        builder.HasOne(c => c.CollaboratorContract).WithMany(c => c.Collaborators).HasForeignKey(c => c.CollaboratorContractId);

        builder.HasOne(c => c.CollaboratorState).WithMany(c => c.Collaborators).HasForeignKey(c => c.CollaboratorStateId);

        builder.HasOne(c => c.Company).WithMany(c => c.Collaborators).HasForeignKey(c => c.CompanyId);

        builder.HasOne(c => c.DocumentType).WithMany(d => d.Collaborators).HasForeignKey(c => c.DocumentTypeId);

        builder.HasOne(c => c.EducationalLevel).WithMany(d => d.Collaborators).HasForeignKey(c => c.EducationalLevelId);

        builder.HasOne(c => c.HealthEntity).WithMany(h => h.Collaborators).HasForeignKey(c => c.HealthEntityId);

        builder.HasOne(c => c.MaritalStatus).WithMany(m => m.Collaborators).HasForeignKey(c => c.MaritalStatusId);

        builder.HasOne(c => c.EconomicLevel).WithMany(m => m.Collaborators).HasForeignKey(c => c.MaritalStatusId);

        builder.HasOne(c => c.Pension).WithMany(p => p.Collaborators).HasForeignKey(c => c.PensionId);

        builder.HasOne(c => c.Bank).WithMany(p => p.Collaborators).HasForeignKey(c => c.BankId);

        builder.HasOne(c => c.ProfessionalAdvice).WithMany(p => p.Collaborators).HasForeignKey(c => c.ProfessionalAdviceId);

        builder.HasOne(c => c.Position).WithMany(p => p.Collaborators).HasForeignKey(c => c.PositionId);

        builder.HasOne(c => c.Role).WithMany(r => r.Collaborators).HasForeignKey(c => c.RoleId);

        builder.HasOne(c => c.SeveranceBenefit).WithMany(p => p.Collaborators).HasForeignKey(c => c.SeveranceBenefitId);

        builder.HasOne(c => c.TypeAccount).WithMany(p => p.Collaborators).HasForeignKey(c => c.TypeAccountId);


        builder.Property(c => c.BusinessEmail).HasConversion(
           email => email.Value,
           value => Email.Create(value)!)
           .HasMaxLength(100);
        builder.HasIndex(c => c.BusinessEmail).IsUnique();

        builder.Property(c => c.PersonalEmail)
           .HasConversion(
           email => email.Value,
           value => Email.Create(value)!)
           .HasMaxLength(100)
           .IsRequired(false);

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.Document).HasMaxLength(100);

        //builder.Property(c => c.PhoneNumber).HasConversion(
        //   phoneNumber => phoneNumber.Value,
        //   value => PhoneNumber.Create(value)!)
        //   .HasMaxLength(50);


        builder.OwnsOne(c => c.Address, addressBuilder => {

            addressBuilder.Property(a => a.StreetAddress).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.CountryCode);
            addressBuilder.Property(a => a.Country).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.StateCode);
            addressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.CityCode);
            addressBuilder.Property(a => a.City).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.ZipCode).HasMaxLength(100).IsRequired(false);
        });

        builder.Property(c => c.CVFile).IsRequired(false);

        builder.Property(c => c.ProfessionalCard).IsRequired(false);

        builder.Property(c => c.Birthdate).HasConversion(
        entranceDate => entranceDate.Value,
        value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        ).IsRequired(false);
        builder.Property(c => c.Country).IsRequired(false);
        builder.Property(c => c.Department).IsRequired(false);
        builder.Property(c => c.City).IsRequired(false);
        builder.Property(c => c.LocationAddress).HasMaxLength(200).IsRequired(false);
        builder.Property(c => c.PhoneNumber).HasMaxLength(50).IsRequired(false);
        builder.Property(c => c.PostalCode).HasMaxLength(50).IsRequired(false);


        builder.Property(c => c.Photo).IsRequired(false);
        builder.Property(c => c.PhotoName).IsRequired(false);

        builder.Property(c => c.SendNotificationsToPersonalEmail);

        builder.Property(c => c.LoginCode).HasMaxLength(10);

        builder.Property(c => c.OtherDocumentType).HasMaxLength(50);

        builder.Property(c => c.RecoveryCode).HasMaxLength(10);


        builder.Property(c => c.SuspensionReason).HasMaxLength(200);

        builder.Property(c => c.FamilyMembersNumber);

        builder.Property(c => c.FamilyMembersNumber);

        builder.Property(c => c.IsPendingInvitation);

        builder.Property(c => c.ShowNewFeatures);

        builder.Property(c => c.IsSuspended);

        builder.Property(c => c.IsCopasstMember);
        builder.Property(c => c.IsCoexistenceCommitteeMember);
        builder.Property(c => c.IsEvaluator);

        builder.Property(c => c.ChangedBy);
        builder.Property(c => c.EmailChangedBy);

        builder.Property(c => c.EntranceDate).HasConversion(
          entranceDate => entranceDate.Value,
          value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
       );

        builder.Property(c => c.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(c => c.Acts).WithOne(a => a.Collaborator).HasForeignKey(a => a.CollaboratorId);
        builder.HasMany(c => c.AssigneeNotes).WithOne(n => n.Assignee).HasForeignKey(n => n.AssignedTo);
        builder.HasMany(c => c.BrigadeMembers).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorBenefitClaims).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorBrigades).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        //builder.HasMany(c => c.CollaboratorCriterias).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorDreamMapAnswers).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorEvents).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorInductions).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorTalentPools).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CollaboratorTags).WithOne(ct => ct.Collaborator).HasForeignKey(ct => ct.CollaboratorId);
        builder.HasMany(c => c.CreatedNotes).WithOne(n => n.Creator).HasForeignKey(n => n.CreatedBy);
        builder.HasMany(c => c.DocumentManagements).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.EmergencyContacts).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.FormAnswers).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.FormAnswerGroups).WithOne(fag => fag.Collaborator).HasForeignKey(fag => fag.CollaboratorId);
        //builder.HasMany(c => c.Evaluators).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.Notifications).WithOne(n => n.Collaborator).HasForeignKey(n => n.CollaboratorId);
        builder.HasMany(c => c.OccupationalTests).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.OccupationalRecommendations).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.WorkplaceEvidences).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.WorkplaceInformations).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
        builder.HasMany(c => c.WorkplaceRecommendations).WithOne(c => c.Collaborator).HasForeignKey(c => c.CollaboratorId);
    }
}
