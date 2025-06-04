using System;
using System.ComponentModel.DataAnnotations;
using MeeCon.Domain.Model.User;

namespace MeeCon.Domain.Model.Subscription
{
    public class UserSubscription
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int SubscriptionId { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        public bool IsActive { get; set; }
        
        // Navigation properties
        public UDbModel User { get; set; }
        public Subscription Subscription { get; set; }
    }
} 