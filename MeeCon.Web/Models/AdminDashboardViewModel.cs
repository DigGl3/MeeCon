using MeeCon.Domain.Model.Home;
using MeeCon.Domain.Model.User;
using System.Collections.Generic;

namespace MeeCon.Web.Models
{
    public class AdminDashboardViewModel
    {
        public List<User> Users { get; set; }
        public List<Post> Posts { get; set; }
    }
} 