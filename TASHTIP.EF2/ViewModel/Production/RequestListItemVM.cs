namespace TASHTIP.EF.ViewModel.Production
{
    /// <summary>Flat row for the admin requests table: the request plus the unit it's about.</summary>
    public class RequestListItemVM
    {
        public int ID_PR { get; set; }
        public string? CutomerName { get; set; }
        public string? RequestDate { get; set; }
        public string? Status { get; set; }
        public string? ServicesName { get; set; }
        public string? City { get; set; }
        public decimal? Price { get; set; }
    }
}
