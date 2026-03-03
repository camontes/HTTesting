using HR_Platform.Domain.NotificationTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NotificationTypeRepository(ApplicationDbContext context) : INotificationTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<NotificationType?> GetByIdAsync(NotificationTypeId Id) =>
            await _context.NotificationTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == Id);
    }
}
