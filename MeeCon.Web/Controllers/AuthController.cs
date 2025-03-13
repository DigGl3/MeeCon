using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using System;

namespace MeeCon.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string email, string password)
        {
            try
            {
                var user = _authService.Register(username, email, password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var user = _authService.Login(email, password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
