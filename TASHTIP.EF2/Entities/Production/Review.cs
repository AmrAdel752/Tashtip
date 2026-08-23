using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASHTIP.EF.Entities.Production
{
    /// <summary>Customer review of a finished unit, moderated before it shows on the public Home page.</summary>
    public class Review
    {
        [Key]
        public int ID { get; set; }

        /// <summary>Optional link to the unit being reviewed; a review can also be general feedback.</summary>
        public int? BussinessGallaryID { get; set; }

        [ForeignKey(nameof(BussinessGallaryID))]
        public BussinessGallary? BussinessGallary { get; set; }

        [MaxLength(450)]
        public string? UserId { get; set; }

        [MaxLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; }
    }
}
