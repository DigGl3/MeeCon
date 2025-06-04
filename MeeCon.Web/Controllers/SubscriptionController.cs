using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model;

namespace MeeConPjnw.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<ActionResult> Index()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            return View(subscriptions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Subscribe(int subscriptionId)
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var success = await _subscriptionService.SubscribeUserAsync(loggedInUserId, subscriptionId);
            
            if (success)
            {
                TempData["Message"] = "Successfully subscribed!";
            }
            else
            {
                TempData["Error"] = "Failed to subscribe. Please try again.";
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cancel()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var success = await _subscriptionService.CancelSubscriptionAsync(loggedInUserId);
            
            if (success)
            {
                TempData["Message"] = "Successfully cancelled subscription!";
            }
            else
            {
                TempData["Error"] = "Failed to cancel subscription. Please try again.";
            }
            
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Status()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var activeSubscription = await _subscriptionService.GetUserActiveSubscriptionAsync(loggedInUserId);
            var remainingPosts = await _subscriptionService.GetUserRemainingPostsAsync(loggedInUserId);
            
            ViewBag.RemainingPosts = remainingPosts;
            return View(activeSubscription);
        }
    }
} 