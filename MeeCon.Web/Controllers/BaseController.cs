using MeeCon.BusinessLogic.Core;
using System.Web.Mvc;

namespace MeeCon.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int GetLoggedInUserId()
        {
            var userId = AuthHelper.GetLoggedInUserId();
            if (!userId.HasValue)
            {
                throw new System.UnauthorizedAccessException("User is not logged in");
            }
            return userId.Value;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Skip authentication check for Auth controller actions
            if (filterContext.Controller is AuthController)
                return;

            if (!AuthHelper.IsUserLoggedIn())
            {
                filterContext.Result = RedirectToAction("Login", "Auth");
            }
        }
    }
} 