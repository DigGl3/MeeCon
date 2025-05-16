using MeeCon.Domain.Model.User;
using MeeCon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        //Foreign keys
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Post Post { get; set; }
        public UDbModel User { get; set; }
    }
}
