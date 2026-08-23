using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Employee;
using TASHTIP.EF.Entities.Production;
using TASHTIP.EF.ViewModel.Production;
using TASHTIP.EF.ViewModel.UserManagment;
using TASHTIP.RepoUOWCore.Services;

namespace TASHTIP.Controllers
{
    /// <summary>
    /// The back-office: request pipeline, gallery/media management, review moderation,
    /// appointments and user/role administration. Gated behind the same
    /// "Permissions.Admin" claim policy the header nav already checks for.
    /// </summary>
    [Authorize(Policy = "Permissions.Admin")]
    public class AdminController : Controller
    {
        private readonly RequestsService _requests;
        private readonly GalleryService _gallery;
        private readonly ReviewsService _reviews;
        private readonly AppointmentsService _appointments;
        private readonly DashboardStatsService _stats;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            RequestsService requests,
            GalleryService gallery,
            ReviewsService reviews,
            AppointmentsService appointments,
            DashboardStatsService stats,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _requests = requests;
            _gallery = gallery;
            _reviews = reviews;
            _appointments = appointments;
            _stats = stats;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var overview = await _stats.GetOverviewAsync();
            return View(overview);
        }

        #region Requests

        [HttpGet]
        public async Task<IActionResult> Requests(string? status)
        {
            ViewBag.SelectedStatus = status;
            var requests = await _requests.GetAllWithGalleryAsync(status);
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> RequestDetails(int id)
        {
            var request = await _requests.GetByIdAsync(id);
            if (request == null) return NotFound();

            var gallery = request.BussinessGallaryID.HasValue
                ? await _gallery.GetByIdAsync(request.BussinessGallaryID.Value)
                : null;

            var vm = new DetailsProjectFinishVM
            {
                PurchaseRequest = request,
                BussinessGallary = gallery
            };
            ViewBag.History = await _requests.GetHistoryAsync(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRequestStatus(int id, string status, string? note)
        {
            if (!RequestStatus.All.Contains(status))
            {
                return Json(new { error = "Invalid status" });
            }

            var admin = await _userManager.GetUserAsync(User);
            var ok = await _requests.ChangeStatusAsync(id, status, admin?.Id, admin?.UserName, note);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        #endregion

        #region Gallery / units

        [HttpGet]
        public async Task<IActionResult> Gallery()
        {
            var items = await _gallery.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> EditGallery(int id)
        {
            var item = await _gallery.GetByIdAsync(id);
            if (item == null) return NotFound();
            ViewBag.Images = await _gallery.GetImagesAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGalleryMedia(int id, string? model3DPath, string? panorama360Path)
        {
            var ok = await _gallery.UpdateMediaPathsAsync(id, model3DPath, panorama360Path);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        [HttpPost]
        public async Task<IActionResult> AddGalleryImage(int id, string imagePath, int sortOrder = 0)
        {
            await _gallery.AddImageAsync(id, imagePath, sortOrder);
            return Json(new { success = "Success" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGalleryImage(int imageId)
        {
            var ok = await _gallery.DeleteImageAsync(imageId);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGallery(int id)
        {
            var ok = await _gallery.DeleteAsync(id);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        #endregion

        #region Reviews

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            var pending = await _reviews.GetPendingAsync();
            return View(pending);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReview(int id)
        {
            var ok = await _reviews.ApproveAsync(id);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        [HttpPost]
        public async Task<IActionResult> RejectReview(int id)
        {
            var ok = await _reviews.RejectAsync(id);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        #endregion

        #region Appointments

        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            var items = await _appointments.GetAllAsync();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppointmentStatus(int id, string status)
        {
            var ok = await _appointments.UpdateStatusAsync(id, status);
            return ok ? Json(new { success = "Success" }) : Json(new { error = "Not found" });
        }

        #endregion

        #region Users & roles

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var result = new System.Collections.Generic.List<UsersViewModel>();
            foreach (var user in users)
            {
                result.Add(new UsersViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                });
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAdminRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Json(new { error = "Not found" });

            var adminRole = TASHTIP.EF.UsersRolePolicy.Roles.Admin.ToString();
            if (await _userManager.IsInRoleAsync(user, adminRole))
            {
                await _userManager.RemoveFromRoleAsync(user, adminRole);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, adminRole);
            }
            return Json(new { success = "Success" });
        }

        #endregion
    }
}
