using System;
using System.ComponentModel.DataAnnotations;

namespace TASHTIP.EF.Entities.Production
{
    public static class AppointmentStatus
    {
        public const string Pending = "Pending";
        public const string Confirmed = "Confirmed";
        public const string Done = "Done";
        public const string Cancelled = "Cancelled";
    }

    /// <summary>A booked site-visit / consultation with an engineer.</summary>
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(450)]
        public string? UserId { get; set; }

        [Required, MaxLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? Phone { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(150)]
        public string? EngineerName { get; set; }

        public DateTime PreferredDate { get; set; }

        [MaxLength(30)]
        public string? TimeSlot { get; set; }

        [MaxLength(30)]
        public string Status { get; set; } = AppointmentStatus.Pending;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
