using MeeCon.Domain.Model.User;
using System.Collections.Generic;

namespace MeeCon.Web.Controllers
{
    public interface IUserRepository
    {
        void DeleteUser(int userId);
        List<UDbModel> GetAllUsers();
    }
}