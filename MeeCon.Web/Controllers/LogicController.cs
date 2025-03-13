using MeeCon.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeeCon.Web.Controllers
{
    public class LogicController : Controller
    {
        private readonly ISession _session;
        public LogicController()
        {
            var b1 = new BusinessLogic();
            _session = b1.GetSessionBL();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Action Result Index(string username, string password)
        {
            try
            {
                var user = _session.Login(username, password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}