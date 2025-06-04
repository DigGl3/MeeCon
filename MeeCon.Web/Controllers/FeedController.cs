using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Home;

namespace MeeConPjnw.Controllers
{
    public class FeedController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISubscriptionService _subscriptionService;

        public FeedController(IPostService postService, ISubscriptionService subscriptionService)
        {
            _postService = postService;
            _subscriptionService = subscriptionService;
        }

        public async Task<ActionResult> Index()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var allPosts = await _postService.GetAllVisiblePostsAsync(loggedInUserId);
            var isAdFree = await _subscriptionService.IsUserAdFreeAsync(loggedInUserId);
            
            ViewBag.IsAdFree = isAdFree;
            return View(allPosts);
        }
    }
} 