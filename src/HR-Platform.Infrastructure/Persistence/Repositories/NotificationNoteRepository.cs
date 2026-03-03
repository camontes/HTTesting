using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NotificationNotes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NotificationNoteRepository(ApplicationDbContext context) : INotificationNoteRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<NotificationNote?> GetByIdAsync(NotificationNoteId Id) =>
            await _context.NotificationNotes
            .SingleOrDefaultAsync(n => n.Id == Id);

        public void Add(NotificationNote notificationNote) => _context.NotificationNotes.Add(notificationNote);

        public async Task<List<NotificationNote>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.NotificationNotes.Where(x => x.CollaboratorId == collaboratorId && !x.IsRead).ToListAsync();
        public async Task<List<NotificationNote>> GetAllReadByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.NotificationNotes.Where(x => x.CollaboratorId == collaboratorId && x.IsRead).ToListAsync();
        public void Update(NotificationNote notificationNote) => _context.NotificationNotes.Update(notificationNote);
        public void UpdateRange(List<NotificationNote> notifications) => _context.NotificationNotes.UpdateRange(notifications);
        public void Delete(NotificationNote notificationNote) => _context.NotificationNotes.Remove(notificationNote);

    }
}
