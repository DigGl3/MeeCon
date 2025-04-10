using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeeCon.Web.Models.Auth
{
	public class UserDataLogin
	{
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}