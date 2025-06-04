using MeeCon.Domain.Model.Post;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface IAdminServices
    {
        List<UDbModel> GetAllUsers();
        List<Post> GetAllPosts();
        void DeleteUser(int userId);
        void DeletePost(int postId);
    }

}
