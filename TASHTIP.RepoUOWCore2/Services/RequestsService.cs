using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;

namespace TASHTIP.RepoUOWCore.Services
{
    /// <summary>
    /// Business logic for PurchaseRequest: listing/filtering, ownership lookup and the
    /// status-change workflow (which also writes the audit trail and a notification).
    /// This is the first real tenant of TASHTIP.RepoUOWCore - previously the whole
    /// "Business Logic Layer" was an empty project and controllers talked to
    /// GeneralDBContext directly.
    /// </summary>
    public class RequestsService
    {
        private readonly GeneralDBContext _db;
        private readonly NotificationsService _notifications;

        public RequestsService(GeneralDBContext db, NotificationsService notifications)
        {
            _db = db;
            _notifications = notifications;
        }

        public async Task<List<PurchaseRequest>> GetAllAsync(string? status = null)
        {
            var query = _db.PurchaseRequest.AsQueryable();
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(r => r.Status == status);
            }
            return await query.OrderByDescending(r => r.ID_PR).ToListAsync();
        }

        public async Task<List<PurchaseRequest>> GetByUserAsync(string userId)
        {
            return await _db.PurchaseRequest
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.ID_PR)
                .ToListAsync();
        }

        public Task<PurchaseRequest?> GetByIdAsync(int id)
        {
            return _db.PurchaseRequest.FirstOrDefaultAsync(r => r.ID_PR == id);
        }

        public async Task<List<RequestStatusHistory>> GetHistoryAsync(int purchaseRequestId)
        {
            return await _db.RequestStatusHistory
                .Where(h => h.PurchaseRequestId == purchaseRequestId)
                .OrderBy(h => h.ChangedAt)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetCountsByStatusAsync()
        {
            return await _db.PurchaseRequest
                .GroupBy(r => r.Status ?? RequestStatus.New)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        /// <summary>Moves a request to a new status, records the change and notifies the owning customer.</summary>
        public async Task<bool> ChangeStatusAsync(int purchaseRequestId, string newStatus, string? changedByUserId, string? changedByName, string? note = null)
        {
            var request = await _db.PurchaseRequest.FirstOrDefaultAsync(r => r.ID_PR == purchaseRequestId);
            if (request == null)
            {
                return false;
            }

            var oldStatus = request.Status;
            if (oldStatus == newStatus)
            {
                return true;
            }

            request.Status = newStatus;

            _db.RequestStatusHistory.Add(new RequestStatusHistory
            {
                PurchaseRequestId = purchaseRequestId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedByUserId = changedByUserId,
                ChangedByName = changedByName,
                ChangedAt = DateTime.Now,
                Note = note
            });

            await _db.SaveChangesAsync();

            if (!string.IsNullOrEmpty(request.UserId))
            {
                var label = RequestStatus.ArabicLabel.TryGetValue(newStatus, out var l) ? l : newStatus;
                await _notifications.CreateAsync(
                    request.UserId,
                    "تحديث حالة طلبك",
                    $"تم تحديث حالة طلبك رقم #{request.ID_PR} إلى: {label}",
                    "/Account/MyRequests");
            }

            return true;
        }
    }
}
