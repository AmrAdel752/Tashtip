using System;
using System.ComponentModel.DataAnnotations;

namespace TASHTIP.EF.Entities.Production
{
    /// <summary>In-app notification shown in the header bell (e.g. "your request status changed").</summary>
    public class Notification
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Message { get; set; }

        /// <summary>Where clicking the notification should take the user, e.g. /Account/MyRequests.</summary>
        [MaxLength(300)]
        public string? LinkUrl { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
