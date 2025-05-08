using System;
using System.Collections.Generic;
using System.Linq;
using MeeCon.BusinessLogic;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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

        public async Task<ActionResult> Index()
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

    }
}