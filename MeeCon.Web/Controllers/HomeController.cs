using System;
using System.Linq;
using MeeCon.BusinessLogic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using MeeCon.Web.Models;
using MeeCon.Domain.Model.Home;
using Microsoft.AspNetCore.Mvc;

namespace MeeConPjnw.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public HomeController()
        {
            _context = new DataContext();
        }

        public ActionResult Index()
        {
            var allPost = _context.Posts
                .Include(p => p.User)
                .ToList();

            return View(allPost);

        }
        public ActionResult About()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult  CreatePost(PostVM post)
        {
            int loggedInUser = 1011;
            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdate = DateTime.UtcNow,
                UserId = loggedInUser,
                ImageUrl = "",
                NrOfReports = 0
            };

            _context.Posts.Add(newPost);
             _context.SaveChangesAsync(); 

            return RedirectToAction("Index");
        }


    }
}