using MeeCon.Domain.Model.User;
using System;
using System.Web;
using MeeCon.Domain.Model.Home;

namespace MeeCon.BusinessLogic.Core
{
    public static class AuthHelper
    {
        private const string UserIdKey = "LoggedInUserId";
        private const string UsernameKey = "LoggedInUsername";
        private const string UserRoleKey = "UserRole";

        public static void SetLoggedInUser(UDbModel user)
        {
            HttpContext.Current.Session[UserIdKey] = user.UserId;
            HttpContext.Current.Session[UsernameKey] = user.Username;
            HttpContext.Current.Session[UserRoleKey] = user.Role;
        }

        public static int? GetLoggedInUserId()
        {
            return HttpContext.Current.Session[UserIdKey] as int?;
        }

        public static string GetLoggedInUsername()
        {
            return HttpContext.Current.Session[UsernameKey] as string;
        }

        public static string GetLoggedInUserRole()
        {
            return HttpContext.Current.Session[UserRoleKey] as string ?? AppRoles.User;
        }

        public static bool IsUserLoggedIn()
        {
            return GetLoggedInUserId().HasValue;
        }

        public static bool IsUserAdmin()
        {
            return GetLoggedInUserRole() == AppRoles.Admin;
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Clear();
        }

        public static string GetLoginUrl()
        {
            return "/Auth/Login";
        }
    }
} 