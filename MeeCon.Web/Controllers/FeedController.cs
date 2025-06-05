using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Home;
using MeeCon.Web.Controllers;

namespace MeeConPjnw.Controllers
{
    public class FeedController : BaseController
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
            var userId = GetLoggedInUserId();
            var allPosts = await _postService.GetAllVisiblePostsAsync(userId);
            var isAdFree = await _subscriptionService.IsUserAdFreeAsync(userId);
            
            ViewBag.IsAdFree = isAdFree;
            return View(allPosts);
        }
    }
} 