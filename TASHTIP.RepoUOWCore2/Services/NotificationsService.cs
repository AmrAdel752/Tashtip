using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;

namespace TASHTIP.RepoUOWCore.Services
{
    /// <summary>In-app notifications shown in the header bell.</summary>
    public class NotificationsService
    {
        private readonly GeneralDBContext _db;

        public NotificationsService(GeneralDBContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(string userId, string title, string? message, string? linkUrl = null)
        {
            _db.Notification.Add(new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                LinkUrl = linkUrl
            });
            await _db.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetForUserAsync(string userId, int take = 10)
        {
            return await _db.Notification
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(take)
                .ToListAsync();
        }

        public Task<int> UnreadCountAsync(string userId)
        {
            return _db.Notification.CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task MarkReadAsync(int id, string userId)
        {
            var notification = await _db.Notification.FirstOrDefaultAsync(n => n.ID == id && n.UserId == userId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _db.SaveChangesAsync();
            }
        }

        public async Task MarkAllReadAsync(string userId)
        {
            var unread = await _db.Notification.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
            foreach (var n in unread)
            {
                n.IsRead = true;
            }
            await _db.SaveChangesAsync();
        }
    }
}
