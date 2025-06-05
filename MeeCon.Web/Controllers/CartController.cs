using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Cart;
using System.Collections.Generic;

namespace MeeCon.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: Cart
        public async Task<ActionResult> Index()
        {
            var userId = GetLoggedInUserId();
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            var total = await _cartService.GetCartTotalAsync(userId);
            
            ViewBag.Total = total;
            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart(int subscriptionId)
        {
            var userId = GetLoggedInUserId();
            await _cartService.AddToCartAsync(userId, subscriptionId);
            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = GetLoggedInUserId();
            await _cartService.RemoveFromCartAsync(userId, cartItemId);
            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ClearCart()
        {
            var userId = GetLoggedInUserId();
            await _cartService.ClearCartAsync(userId);
            return RedirectToAction("Index");
        }

        // GET: Cart/Checkout
        public async Task<ActionResult> Checkout()
        {
            var userId = GetLoggedInUserId();
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            var total = await _cartService.GetCartTotalAsync(userId);
            
            ViewBag.Total = total;
            return View(cartItems);
        }

        // POST: Cart/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProcessPayment(string paymentMethod)
        {
            var userId = GetLoggedInUserId();
            var success = await _cartService.ProcessPaymentAsync(userId, paymentMethod);
            
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
            var userId = GetLoggedInUserId();
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return Json(cartItems.Count, JsonRequestBehavior.AllowGet);
        }
    }
} 