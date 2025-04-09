using MeeCon.Web.Models.Auth;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeeCon.BusinessLogic.Core;
using MeeCon.Domain.Enum;

namespace MeeCon.Web.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string fullName, string password)
        {
            if(ModelState.IsValid)
            {
                var userApi = new UserApi();
                userApi.RegisterUser(new ULoginData
                {
                    Credential = fullName,
                    Password = password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.UtcNow
                });

                return RedirectToAction("Login");
            }
            return View();
        }


    }
    
}