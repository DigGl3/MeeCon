using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.Web.Models;
using MeeCon.Domain.Model.Home;
using MeeCon.BusinessLogic.Interfaces;

namespace MeeConPjnw.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<ActionResult> Index()
        {
            int loggedInUserId = 1011; // TODO: Obține ID-ul utilizatorului logat din sesiune/autentificare reală
            var allPosts = await _postService.GetAllVisiblePostsAsync(loggedInUserId);
            return View(allPosts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(PostVM post)
        {
            int loggedInUserId = 1011;
            await _postService.CreatePostAsync(post, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> TogglePostLike(PostLikeVM postLikeVM)
        {
            int loggedInUserId = 1011;
            await _postService.ToggleLikeAsync(postLikeVM.PostId, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddPostComment(PostCommentVM postCommentVM)
        {
            int loggedInUserId = 1011;
            await _postService.AddCommentAsync(postCommentVM, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> RemovePostComment(RemoveCommentVM removeCommentVM)
        {
            await _postService.RemoveCommentAsync(removeCommentVM.CommentId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> TogglePostFavorite(PostFavoriteVM postFavoriteVM)
        {
            int loggedInUserId = 1011;
            await _postService.ToggleFavoriteAsync(postFavoriteVM.PostId, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> TogglePostVisibility(PostVisibilityVM postVisibilityVM)
        {
            int loggedInUserId = 1011;
            await _postService.ToggleVisibilityAsync(postVisibilityVM.PostId, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddPostReport(PostReportVM postReportVM)
        {
            int loggedInUserId = 1011;
            await _postService.AddReportAsync(postReportVM.PostId, loggedInUserId);
            return RedirectToAction("Index");
        }
    }
}
