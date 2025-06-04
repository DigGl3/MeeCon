using MeeCon.Domain.Model.Post;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MeeCon.Domain.Model.Post
{
    public class Favorite
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Post Post { get; set; }
        public UDbModel User { get; set; }
    }
}