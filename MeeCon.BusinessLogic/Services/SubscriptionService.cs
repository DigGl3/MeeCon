using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Subscription;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DataContext _context;

        public SubscriptionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(int id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        public async Task<UserSubscription> GetUserActiveSubscriptionAsync(int userId)
        {
            return await _context.UserSubscriptions
                .Include(us => us.Subscription)
                .FirstOrDefaultAsync(us => us.UserId == userId && us.IsActive && us.EndDate > DateTime.UtcNow);
        }

        public async Task<bool> SubscribeUserAsync(int userId, int subscriptionId)
        {
            var subscription = await GetSubscriptionByIdAsync(subscriptionId);
            if (subscription == null) return false;

            var existingSubscription = await GetUserActiveSubscriptionAsync(userId);
            if (existingSubscription != null)
            {
                existingSubscription.IsActive = false;
                _context.Entry(existingSubscription).State = EntityState.Modified;
            }

            var userSubscription = new UserSubscription
            {
                UserId = userId,
                SubscriptionId = subscriptionId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };

            _context.UserSubscriptions.Add(userSubscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelSubscriptionAsync(int userId)
        {
            var activeSubscription = await GetUserActiveSubscriptionAsync(userId);
            if (activeSubscription == null) return false;

            activeSubscription.IsActive = false;
            _context.Entry(activeSubscription).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsUserAdFreeAsync(int userId)
        {
            var activeSubscription = await GetUserActiveSubscriptionAsync(userId);
            return activeSubscription?.Subscription.IsAdFree ?? false;
        }

        public async Task<int> GetUserRemainingPostsAsync(int userId)
        {
            var activeSubscription = await GetUserActiveSubscriptionAsync(userId);
            if (activeSubscription == null) return 0;

            if (activeSubscription.Subscription.PostLimit == -1) return -1; // Unlimited

            var postsThisMonth = await _context.Posts
                .CountAsync(p => p.UserId == userId && 
                               p.DateCreated >= activeSubscription.StartDate && 
                               p.DateCreated <= activeSubscription.EndDate);

            return Math.Max(0, activeSubscription.Subscription.PostLimit - postsThisMonth);
        }
    }
} 