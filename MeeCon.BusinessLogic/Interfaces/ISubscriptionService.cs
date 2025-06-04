using MeeCon.Domain.Model.Subscription;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> GetSubscriptionByIdAsync(int id);
        Task<UserSubscription> GetUserActiveSubscriptionAsync(int userId);
        Task<bool> SubscribeUserAsync(int userId, int subscriptionId);
        Task<bool> CancelSubscriptionAsync(int userId);
        Task<bool> IsUserAdFreeAsync(int userId);
        Task<int> GetUserRemainingPostsAsync(int userId);
    }
} 