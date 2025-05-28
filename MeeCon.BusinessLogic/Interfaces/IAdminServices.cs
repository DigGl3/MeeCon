using MeeCon.Domain.Model.User;
using MeeCon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        List<UDbModel> GetAllUsers();
        List<Post> GetAllPosts();
        void DeleteUser(int userId);
        void DeletePost(int postId);
    }

}
