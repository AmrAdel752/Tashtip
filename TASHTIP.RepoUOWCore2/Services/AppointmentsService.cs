using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;

namespace TASHTIP.RepoUOWCore.Services
{
    /// <summary>Booking a site-visit / consultation with an engineer.</summary>
    public class AppointmentsService
    {
        private readonly GeneralDBContext _db;

        public AppointmentsService(GeneralDBContext db)
        {
            _db = db;
        }

        public async Task<Appointment> BookAsync(Appointment appointment)
        {
            appointment.Status = AppointmentStatus.Pending;
            _db.Appointment.Add(appointment);
            await _db.SaveChangesAsync();
            return appointment;
        }

        public Task<List<Appointment>> GetAllAsync()
        {
            return _db.Appointment.OrderByDescending(a => a.PreferredDate).ToListAsync();
        }

        public Task<List<Appointment>> GetByUserAsync(string userId)
        {
            return _db.Appointment
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.PreferredDate)
                .ToListAsync();
        }

        public Task<int> GetUpcomingCountAsync()
        {
            return _db.Appointment.CountAsync(a =>
                a.Status != AppointmentStatus.Cancelled &&
                a.Status != AppointmentStatus.Done &&
                a.PreferredDate >= System.DateTime.Today);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var appointment = await _db.Appointment.FindAsync(id);
            if (appointment == null) return false;
            appointment.Status = status;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
