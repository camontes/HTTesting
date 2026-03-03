using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository(ApplicationDbContext context) : INotificationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Notification?> GetByIdAsync(NotificationId Id) =>
            await _context.Notifications
            .SingleOrDefaultAsync(n => n.Id == Id);

        public void Add(Notification notification) => _context.Notifications.Add(notification);

        public async Task<List<Notification>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.Notifications.Where(x => x.CollaboratorId == collaboratorId).ToListAsync();
        public async Task<List<Notification>> GetAllReadByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.Notifications.Where(x => x.CollaboratorId == collaboratorId && x.IsRead).ToListAsync();
        public void Update(Notification notification) => _context.Notifications.Update(notification);
        public void Delete(Notification notification) => _context.Notifications.Remove(notification);
    }
}
