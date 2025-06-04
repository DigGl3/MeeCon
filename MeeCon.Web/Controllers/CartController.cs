using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Cart;
using System.Collections.Generic;

namespace MeeCon.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: Cart
        public async Task<ActionResult> Index()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var cartItems = await _cartService.GetCartItemsAsync(loggedInUserId);
            var total = await _cartService.GetCartTotalAsync(loggedInUserId);
            
            ViewBag.Total = total;
            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart(int subscriptionId)
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            await _cartService.AddToCartAsync(loggedInUserId, subscriptionId);
            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveFromCart(int cartItemId)
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            await _cartService.RemoveFromCartAsync(loggedInUserId, cartItemId);
            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ClearCart()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            await _cartService.ClearCartAsync(loggedInUserId);
            return RedirectToAction("Index");
        }

        // GET: Cart/Checkout
        public async Task<ActionResult> Checkout()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var cartItems = await _cartService.GetCartItemsAsync(loggedInUserId);
            var total = await _cartService.GetCartTotalAsync(loggedInUserId);
            
            ViewBag.Total = total;
            return View(cartItems);
        }

        // POST: Cart/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProcessPayment(string paymentMethod)
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var success = await _cartService.ProcessPaymentAsync(loggedInUserId, paymentMethod);
            
            if (success)
            {
                TempData["Message"] = "Payment processed successfully!";
                return RedirectToAction("Index", "Subscription", new { area = "" });
            }
            else
            {
                TempData["Error"] = "Payment processing failed. Please try again.";
                return RedirectToAction("Checkout");
            }
        }

        // GET: Cart/GetCartCount
        [HttpGet]
        public async Task<JsonResult> GetCartCount()
        {
            int loggedInUserId = 1011; // This should come from your authentication system
            var cartItems = await _cartService.GetCartItemsAsync(loggedInUserId);
            return Json(cartItems.Count, JsonRequestBehavior.AllowGet);
        }
    }
} 