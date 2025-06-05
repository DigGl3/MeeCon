using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using MeeCon.Domain.Model.Post;
using MeeCon.Domain.Enum;
using MeeCon.Domain.Model.Home;

namespace MeeCon.Domain.Model.User
{
    public class UDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Username")]
        [StringLength(30, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Role { get; set; } = AppRoles.User;
        
        public virtual ICollection<MeeCon.Domain.Model.Post.Post> Posts { get; set; } = new List<MeeCon.Domain.Model.Post.Post>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    }   
}
