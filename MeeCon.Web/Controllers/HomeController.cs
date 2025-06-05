using System.Threading.Tasks;
using System.Web.Mvc;
using MeeCon.Web.Models;
using MeeCon.Domain.Model.Home;
using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Web.Controllers;

namespace MeeConPjnw.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<ActionResult> Index()
        {
            var userId = GetLoggedInUserId();
            var allPosts = await _postService.GetAllVisiblePostsAsync(userId);
            return View(allPosts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(PostVM post)
        {
            var userId = GetLoggedInUserId();
            await _postService.CreatePostAsync(post, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TogglePostLike(PostLikeVM postLikeVM)
        {
            var userId = GetLoggedInUserId();
            await _postService.ToggleLikeAsync(postLikeVM.PostId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPostComment(PostCommentVM postCommentVM)
        {
            var userId = GetLoggedInUserId();
            await _postService.AddCommentAsync(postCommentVM, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePostComment(RemoveCommentVM removeCommentVM)
        {
            await _postService.RemoveCommentAsync(removeCommentVM.CommentId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TogglePostFavorite(PostFavoriteVM postFavoriteVM)
        {
            var userId = GetLoggedInUserId();
            await _postService.ToggleFavoriteAsync(postFavoriteVM.PostId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TogglePostVisibility(PostVisibilityVM postVisibilityVM)
        {
            var userId = GetLoggedInUserId();
            await _postService.ToggleVisibilityAsync(postVisibilityVM.PostId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPostReport(PostReportVM postReportVM)
        {
            var userId = GetLoggedInUserId();
            await _postService.AddReportAsync(postReportVM.PostId, userId);
            return RedirectToAction("Index");
        }
    }
}
