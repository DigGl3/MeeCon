using MeeCon.Web.Models.Auth;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeeCon.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuth _auth;
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _auth = bl.GetAuthBL();
        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Auth(UserDataLogin login)
        {
            var data = new UserLoginBL
            {
                Username = login.Username,
                Password = login.Password,
                UserIP = "localhost"
            };
            string token = _auth.UserAuthLogic(data);
            return View();
        }
        
    }
    
}