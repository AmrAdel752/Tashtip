using System.Collections.Generic;

namespace TASHTIP.EF.Entities.Production
{
    /// <summary>
    /// The allowed values of PurchaseRequest.Status, formalized so the admin
    /// dashboard, the customer timeline and HomeController's "New" filters all
    /// agree on the same vocabulary.
    /// </summary>
    public static class RequestStatus
    {
        public const string New = "New";
        public const string UnderReview = "UnderReview";
        public const string QuoteSent = "QuoteSent";
        public const string Approved = "Approved";
        public const string InProgress = "InProgress";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";

        public static readonly IReadOnlyList<string> All = new[]
        {
            New, UnderReview, QuoteSent, Approved, InProgress, Completed, Cancelled
        };

        public static readonly IReadOnlyDictionary<string, string> ArabicLabel = new Dictionary<string, string>
        {
            [New] = "جديد",
            [UnderReview] = "قيد المراجعة",
            [QuoteSent] = "تم إرسال عرض السعر",
            [Approved] = "تمت الموافقة",
            [InProgress] = "قيد التنفيذ",
            [Completed] = "مكتمل",
            [Cancelled] = "ملغي"
        };
    }
}
