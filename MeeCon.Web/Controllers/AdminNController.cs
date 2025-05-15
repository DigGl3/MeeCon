using Microsoft.AspNetCore.Mvc;
using MeeCon.Web.Models;
using System.Web.Mvc;
using MeeCon.BusinessLogic;
using System.Linq;

namespace MeeCon.Web.Controllers
{
    public class AdminNController : Controller
    {
        private readonly DataContext _context;

        public AdminNController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null || (int)Session["UserId"] != 1011)
            {
                ViewBag.ErrorMessage = "Trebuie să fii conectat cu userId 1011 pentru a avea acces la această pagină.";
                return RedirectToAction("Login", "Auth");
            }

            var users = _context.Users.ToList();
            var posts = _context.Posts.ToList();

            var viewModel = new AdminDashboardViewModel
            {
                Users = users,
                Posts = posts
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int userId)
        {
            if (userId == 1011)
            {
                return RedirectToAction("Index");
            }
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}