using MeeCon.Web.Models;
using System.Collections.Generic;

namespace MeeCon.Web.Controllers
{
    public interface IPostRepository
    {
        void DeletePost(int postId);
        List<Post> GetAllPosts();
    }
}