using MeeCon.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeeCon.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private readonly IAdminService _adminService;
        //public AdminController(IAdminService adminService)
        //{
        //    _adminService = adminService;
        //}

        public ActionResult Index()
        {
            return View();
        }
    }
}