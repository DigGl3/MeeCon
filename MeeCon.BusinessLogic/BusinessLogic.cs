using MeeCon.BusinessLogic.Interfaces;
using MeeCon.BusinessLogic.BLStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic
{
    public class BusinessLogic
    {
        public IAuth GetAuthBL()
        { 
            return new AuthBL();
        }
    }
}
