using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Employee;
using TASHTIP.EF.Entities.Production;
using TASHTIP.RepoUOWCore.Services;

namespace TASHTIP.Controllers
{
    /// <summary>The customer-facing dashboard: track my own requests, book a visit, my appointments, cost estimator.</summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly RequestsService _requests;
        private readonly AppointmentsService _appointments;
        private readonly ReviewsService _reviews;
        private readonly NotificationsService _notifications;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            RequestsService requests,
            AppointmentsService appointments,
            ReviewsService reviews,
            NotificationsService notifications,
            UserManager<ApplicationUser> userManager)
        {
            _requests = requests;
            _appointments = appointments;
            _reviews = reviews;
            _notifications = notifications;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            var userId = _userManager.GetUserId(User);
            var requests = await _requests.GetByUserAsync(userId!);
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> MyRequestDetails(int id)
        {
            var userId = _userManager.GetUserId(User);
            var request = await _requests.GetByIdAsync(id);
            if (request == null || request.UserId != userId) return NotFound();

            ViewBag.History = await _requests.GetHistoryAsync(id);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            var userId = _userManager.GetUserId(User);
            var items = await _appointments.GetByUserAsync(userId!);
            return View(items);
        }

        [HttpGet]
        public IActionResult BookAppointment()
        {
            return View(new Appointment { PreferredDate = DateTime.Today.AddDays(3) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(string? customerName, string phone, DateTime preferredDate, string? timeSlot, string? notes)
        {
            if (string.IsNullOrWhiteSpace(phone) || preferredDate < DateTime.Today)
            {
                ModelState.AddModelError(string.Empty, "من فضلك أدخل رقم هاتف صحيح وتاريخاً في المستقبل.");
                return View(new Appointment { PreferredDate = preferredDate == default ? DateTime.Today.AddDays(3) : preferredDate });
            }

            var user = await _userManager.GetUserAsync(User);

            await _appointments.BookAsync(new Appointment
            {
                UserId = user?.Id,
                CustomerName = string.IsNullOrWhiteSpace(customerName) ? (user?.UserName ?? "عميل") : customerName.Trim(),
                Phone = phone.Trim(),
                Email = user?.Email,
                PreferredDate = preferredDate,
                TimeSlot = timeSlot,
                Notes = notes
            });

            TempData["BookingSuccess"] = "تم استلام طلب حجز المعاينة بنجاح، سنتواصل معك لتأكيد الموعد.";
            return RedirectToAction(nameof(MyAppointments));
        }

        [HttpGet]
        public IActionResult EstimateCost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReview(int? bussinessGallaryId, int rating, string comment)
        {
            var user = await _userManager.GetUserAsync(User);
            await _reviews.SubmitAsync(new Review
            {
                BussinessGallaryID = bussinessGallaryId,
                UserId = user?.Id,
                CustomerName = user?.UserName ?? "عميل",
                Rating = rating,
                Comment = comment
            });
            TempData["ReviewSuccess"] = "شكراً لتقييمك! سيظهر رأيك بعد مراجعته من فريقنا.";
            return RedirectToAction(nameof(MyRequests));
        }

        [HttpGet]
        public async Task<IActionResult> MarkNotificationsRead()
        {
            var userId = _userManager.GetUserId(User);
            await _notifications.MarkAllReadAsync(userId!);
            return RedirectToAction("Home", "Home");
        }
    }
}
