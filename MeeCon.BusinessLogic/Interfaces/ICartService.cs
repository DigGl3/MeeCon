using MeeCon.Domain.Model.Cart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task AddToCartAsync(int userId, int subscriptionId);
        Task RemoveFromCartAsync(int userId, int cartItemId);
        Task ClearCartAsync(int userId);
        Task<decimal> GetCartTotalAsync(int userId);
        Task<bool> ProcessPaymentAsync(int userId, string paymentMethod);
    }
} 