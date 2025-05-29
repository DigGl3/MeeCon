using Microsoft.AspNetCore.Mvc;
using MeeCon.Web.Models;
using System.Web.Mvc;
using MeeCon.BusinessLogic;
using System.Linq;
using MeeCon.BusinessLogic.Interfaces;

namespace MeeCon.Web.Controllers
{
    public class AdminNController : Controller
    {
        private readonly IAdminServices _adminService;

        public AdminNController(IAdminService adminService)
        {
            _adminService = (IAdminServices)adminService;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null || (int)Session["UserId"] != 1011)
            {
                ViewBag.ErrorMessage = "Trebuie să fii conectat cu userId 1011 pentru a avea acces la această pagină.";
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
            if (userId == 1011) return RedirectToAction("Index");

            _adminService.DeleteUser(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int postId)
        {
            _adminService.DeletePost(postId);
            return RedirectToAction(nameof(Index));
        }
    }

}