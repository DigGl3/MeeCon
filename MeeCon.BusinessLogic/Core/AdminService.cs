using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.User;
using MeeCon.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Core
{
    public class AdminService : IAdminServices
    {
        private readonly DataContext _context;

        public AdminService(DataContext context)
        {
            _context = context;
        }

        public List<UDbModel> GetAllUsers() => _context.Users.ToList();

        public List<Post> GetAllPosts() => _context.Posts.ToList();

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void DeletePost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }
    }
}
