using MeeCon.BusinessLogic.Core;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface IAuth
    {
        string UserAuthLogic(UDbModel data);

    }
}
