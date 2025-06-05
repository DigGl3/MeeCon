using Microsoft.AspNetCore.Mvc;
using MeeCon.Web.Models;
using System.Web.Mvc;
using MeeCon.BusinessLogic;
using System.Linq;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.BusinessLogic.Core;
using MeeCon.Domain.Model.Home;

namespace MeeCon.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class AdminNController : BaseController
    {
        private readonly IAdminServices _adminService;

        public AdminNController(IAdminService adminService)
        {
            _adminService = (IAdminServices)adminService;
        }

        public ActionResult Index()
        {
            var userId = GetLoggedInUserId();
            if (!_adminService.IsUserAdmin(userId))
            {
                ViewBag.ErrorMessage = "You do not have permission to access this page.";
                return RedirectToAction("Login", "Auth");
            }

            var viewModel = new AdminDashboardViewModel
            {
                Users = _adminService.GetAllUsers(),
                Posts = _adminService.GetAllPosts()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int userId)
        {
            var currentUserId = GetLoggedInUserId();
            if (!_adminService.IsUserAdmin(currentUserId))
            {
                return RedirectToAction("Index");
            }

            if (userId == currentUserId) return RedirectToAction("Index");

            _adminService.DeleteUser(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int postId)
        {
            var userId = GetLoggedInUserId();
            if (!_adminService.IsUserAdmin(userId))
            {
                return RedirectToAction("Index");
            }

            _adminService.DeletePost(postId);
            return RedirectToAction(nameof(Index));
        }
    }
}