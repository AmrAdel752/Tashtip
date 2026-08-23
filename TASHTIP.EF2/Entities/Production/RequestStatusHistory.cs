using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASHTIP.EF.Entities.Production
{
    /// <summary>
    /// One row per status change on a PurchaseRequest - powers the timeline shown
    /// on both the admin request details page and the customer's "My Requests" page.
    /// </summary>
    public class RequestStatusHistory
    {
        [Key]
        public int ID { get; set; }

        public int PurchaseRequestId { get; set; }

        [ForeignKey(nameof(PurchaseRequestId))]
        public PurchaseRequest? PurchaseRequest { get; set; }

        [MaxLength(30)]
        public string? OldStatus { get; set; }

        [MaxLength(30)]
        public string NewStatus { get; set; } = string.Empty;

        /// <summary>AspNetUsers.Id of whoever changed the status (admin), or null for the system.</summary>
        [MaxLength(450)]
        public string? ChangedByUserId { get; set; }

        [MaxLength(150)]
        public string? ChangedByName { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.Now;

        public string? Note { get; set; }
    }
}
