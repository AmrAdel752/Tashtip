using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASHTIP.EF.Entities.Production
{
    /// <summary>
    /// One extra 2D photo attached to a unit's gallery entry - lets the "2D" tab of
    /// the unit viewer show a real image grid instead of a single ProfileImage.
    /// </summary>
    public class BussinessGallaryImage
    {
        [Key]
        public int ID { get; set; }

        public int BussinessGallaryID { get; set; }

        [ForeignKey(nameof(BussinessGallaryID))]
        public BussinessGallary? BussinessGallary { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}
