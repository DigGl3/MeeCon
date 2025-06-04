using MeeCon.Domain.Model;
using MeeCon.Domain.Model.User;
using System;

namespace MeeCon.Domain.Model.Cart
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime AddedAt { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public UDbModel User { get; set; }
        public MeeCon.Domain.Model.Subscription.Subscription Subscription { get; set; }
    }
} 