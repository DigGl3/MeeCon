using MeeCon.BusinessLogic.Core;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.BLStructure
{
    public class AuthBL : UserApi, IAuth
    {
        public string UserAuthLogic(UserLoginBL data)
        {
            throw new NotImplementedException();
        }
    }
}
