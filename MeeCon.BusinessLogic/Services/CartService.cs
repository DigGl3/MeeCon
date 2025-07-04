using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Cart;
using MeeCon.Domain.Model.Subscription;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly ISubscriptionService _subscriptionService;

        public CartService(DataContext context, ISubscriptionService subscriptionService)
        {
            _context = context;
            _subscriptionService = subscriptionService;
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int userId)
        {
            return await _context.CartItems
                .Include(c => c.Subscription)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task AddToCartAsync(int userId, int subscriptionId)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription == null) return;

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.SubscriptionId == subscriptionId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                _context.Entry(existingItem).State = EntityState.Modified;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    SubscriptionId = subscriptionId,
                    AddedAt = DateTime.UtcNow,
                    Quantity = 1,
                    Price = subscription.Price
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int userId, int cartItemId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .SumAsync(c => c.Price * c.Quantity);
        }

        public async Task<bool> ProcessPaymentAsync(int userId, string paymentMethod)
        {
            var cartItems = await GetCartItemsAsync(userId);
            if (!cartItems.Any()) return false;

            // Here you would integrate with a payment processor
            // For now, we'll just simulate a successful payment
            foreach (var item in cartItems)
            {
                await _subscriptionService.SubscribeUserAsync(userId, item.SubscriptionId);
            }

            await ClearCartAsync(userId);
            return true;
        }
    }
}
