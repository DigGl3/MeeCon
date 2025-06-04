using System;
using System.ComponentModel.DataAnnotations;

namespace MeeCon.Domain.Model.Subscription
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int PostLimit { get; set; } // -1 for unlimited
        
        [Required]
        public bool IsAdFree { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 