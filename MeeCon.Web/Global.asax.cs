using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using MeeCon.BusinessLogic;

namespace MeeCon.Web
{
    public class Global : HttpApplication
    {
        public object BundleConfig { get; private set; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

           // Initialize database with subscription plans
           Database.SetInitializer(new DbInitializer());
           
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            // Set secure cookie flags
            HttpCookie cookie = Request.Cookies["ASP.NET_SessionId"];
            if (cookie != null)
            {
                cookie.Secure = true;
                cookie.HttpOnly = true;
                cookie.SameSite = SameSiteMode.Strict;
            }
        }

        void Application_EndRequest(object sender, EventArgs e)
        {
            // Add security headers
            Response.Headers.Add("X-Frame-Options", "DENY");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
            Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
        }
    }
}