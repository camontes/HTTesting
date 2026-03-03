using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.EmergencyContacts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EmergencyContactRepository(ApplicationDbContext context) : IEmergencyContactRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EmergencyContact EmergencyContact) => _context.EmergencyContacts.Add(EmergencyContact);

        public void Delete(EmergencyContact EmergencyContact) => _context.EmergencyContacts.Remove(EmergencyContact);
        public void Update(EmergencyContact EmergencyContact) => _context.EmergencyContacts.Update(EmergencyContact);
        public void UpdateRangeAsync(List<EmergencyContact> EmergencyContact) => _context.EmergencyContacts.UpdateRange(EmergencyContact);
        public void DeleteRangeEmergencyContacts(List<EmergencyContact> EmergencyContact) => _context.EmergencyContacts.RemoveRange(EmergencyContact);

       
        public async Task<List<EmergencyContact>> GetAll() => await _context.EmergencyContacts
            .AsNoTracking()
            .ToListAsync();

        public async Task<EmergencyContact?> GetByIdAsync(EmergencyContactId Id) =>
            await _context.EmergencyContacts
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeEmergencyContacts(List<EmergencyContact> EmergencyContacts) => _context.EmergencyContacts
            .AddRange(EmergencyContacts);

       

        public async Task<int> GetNumberOfEmergencyContacts(CollaboratorId id) {
           List<EmergencyContact>? amount =  await _context.EmergencyContacts.Where(p => p.CollaboratorId == id).ToListAsync();
           return amount.Count - 1;
        }

        public async Task<List<EmergencyContact>> GetByCollaboratorIdAsync(CollaboratorId id) =>
            await _context.EmergencyContacts.AsNoTracking().Where(x => x.CollaboratorId ==id).ToListAsync();

    }
}
