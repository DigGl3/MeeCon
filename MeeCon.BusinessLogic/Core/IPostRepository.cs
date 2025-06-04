using MeeCon.Domain.Model.Post;
using System.Collections.Generic;

namespace MeeCon.Web.Controllers
{
    public interface IPostRepository
    {
        void DeletePost(int postId);
        List<Post> GetAllPosts();
    }
}