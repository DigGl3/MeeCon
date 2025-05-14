using Microsoft.AspNetCore.Mvc;
using MeeCon.Domain.Model.Home;
using MeeCon.Domain.Model.User;
using MeeCon.Infrastructure.Repository;
using MeeCon.Web.Models;

namespace MeeCon.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public AdminController(IUserRepository userRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetAllUsers();
            var posts = _postRepository.GetAllPosts();
            
            var viewModel = new AdminDashboardViewModel
            {
                Users = users,
                Posts = posts
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeletePost(int postId)
        {
            _postRepository.DeletePost(postId);
            return RedirectToAction(nameof(Index));
        }
    }
}