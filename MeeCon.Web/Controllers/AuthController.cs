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
using System.ComponentModel.DataAnnotations;

namespace MeeCon.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserApi _userApi;

        public AuthController()
        {
            _userApi = new UserApi();
        }

        public ActionResult Login()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDataLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = _userApi.LoginUser(new ULoginData
                {
                    Credential = model.Username,
                    Password = model.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.UtcNow
                });

                if (result.Success)
                {
                    Session["UserId"] = result.UserId;
                    Session["Username"] = result.FullName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = result.StatusMsg;
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDataLogin model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _userApi.RegisterUser(new ULoginData
                    {
                        Email = model.Email,
                        Credential = model.Username,
                        Password = model.Password,
                        LoginIp = Request.UserHostAddress,
                        LoginDateTime = DateTime.UtcNow,
                        FullName = model.Username
                        
                    });

                    if (result.Success)
                    {
                        Session["UserId"] = result.UserId;
                        Session["Username"] = result.FullName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = result.StatusMsg;
                        ModelState.AddModelError("", result.StatusMsg);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "An error occurred during registration: " + ex.Message;
                    ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}