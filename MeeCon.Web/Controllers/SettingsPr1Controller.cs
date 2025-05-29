using MeeCon.BusinessLogic.Core;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Home;
using MeeCon.Domain.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MeeCon.Web.Controllers
{
    [Authorize(Roles = AppRoles.User + "," + AppRoles.Admin)]

    public class SettingsController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IFilesService _filesService;
        private readonly UserManager<UDbModel> _userManager;

        public SettingsController(
            IUsersService usersService,
            IFilesService filesService,
            UserManager<UDbModel> userManager)
        {
            _usersService = usersService;
            _filesService = filesService;
            _userManager = userManager;
        }

        // GET: /Settings
        public async Task<IActionResult> Index()
        {
            var loggedInUser = 1;
            if (loggedInUser == null)
            {
                return (IActionResult)RedirectToAction("Index");
            }

            return (IActionResult)View(loggedInUser);
        }

        // POST: /Settings/UpdateProfilePicture
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfilePicture(UpdateProfilePictureVM model)
        {
            var userId = 1011;
            if (userId == null)
            {
                return RedirectToAction("Index");
            }

            if (model.ProfilePictureImage == null || model.ProfilePictureImage.Length == 0)
            {
                ModelState.AddModelError("ProfilePictureImage", "Please select a valid image.");
                return RedirectToAction(nameof(Index));
            }

            var imageUrl = await _filesService.UploadImageAsync(model.ProfilePictureImage, ImageFileType.ProfilePicture);
        
            return RedirectToAction("Index");
        }
    }
}