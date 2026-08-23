using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;

namespace TASHTIP.RepoUOWCore.Services
{
    public class DashboardOverview
    {
        public int TotalRequests { get; set; }
        public int NewRequests { get; set; }
        public int InProgressRequests { get; set; }
        public int CompletedRequests { get; set; }
        public int TotalUnits { get; set; }
        public int PendingReviews { get; set; }
        public int UpcomingAppointments { get; set; }
        public Dictionary<string, int> RequestsByStatus { get; set; } = new();
        public List<(string Day, int Count)> RequestsLast14Days { get; set; } = new();
    }

    /// <summary>Aggregated numbers/series for the admin dashboard's stat cards and charts.</summary>
    public class DashboardStatsService
    {
        private readonly GeneralDBContext _db;

        public DashboardStatsService(GeneralDBContext db)
        {
            _db = db;
        }

        public async Task<DashboardOverview> GetOverviewAsync()
        {
            var byStatus = await _db.PurchaseRequest
                .GroupBy(r => r.Status ?? RequestStatus.New)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);

            var overview = new DashboardOverview
            {
                TotalRequests = byStatus.Values.Sum(),
                NewRequests = byStatus.GetValueOrDefault(RequestStatus.New),
                InProgressRequests = byStatus.GetValueOrDefault(RequestStatus.InProgress),
                CompletedRequests = byStatus.GetValueOrDefault(RequestStatus.Completed),
                TotalUnits = await _db.BussinessGallary.CountAsync(),
                PendingReviews = await _db.Review.CountAsync(r => !r.IsApproved),
                UpcomingAppointments = await _db.Appointment.CountAsync(a =>
                    a.Status != AppointmentStatus.Cancelled &&
                    a.Status != AppointmentStatus.Done &&
                    a.PreferredDate >= DateTime.Today),
                RequestsByStatus = byStatus
            };

            // "Requests over time": every new request gets an initial RequestStatusHistory
            // row (OldStatus == null) when it's created - see HomeController.PurchaseRequest.
            var since = DateTime.Today.AddDays(-13);
            var createdEvents = await _db.RequestStatusHistory
                .Where(h => h.OldStatus == null && h.ChangedAt >= since)
                .Select(h => h.ChangedAt.Date)
                .ToListAsync();

            var counted = createdEvents.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());
            for (var day = since; day <= DateTime.Today; day = day.AddDays(1))
            {
                overview.RequestsLast14Days.Add((day.ToString("MM/dd"), counted.GetValueOrDefault(day)));
            }

            return overview;
        }
    }
}
