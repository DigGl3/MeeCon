using MeeCon.Domain.Model.User;
using MeeCon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<UDbModel> GetUser(int loggedInUserId);
        Task UpdateUserProfilePicture(int loggedInUserId, string profilePictureUrl);
        Task<List<Post>> GetUserPosts(int userId);
    }
}
