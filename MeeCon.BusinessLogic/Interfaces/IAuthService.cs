using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MeeCon.Domain.Entities;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        User Register(string username, string email, string password);
        User Login(string email, string password);
    }
}
