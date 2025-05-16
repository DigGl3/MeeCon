using System;
using System.Linq;
using MeeCon.BusinessLogic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using MeeCon.Web.Models;
using MeeCon.Domain.Model.Home;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using MeeCon.Web.Controllers.Data;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using MeeCon.Domain.Model;

namespace MeeConPjnw.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController()
        {
            _context = new DataContext(); ;
        }

        public async Task<ActionResult> Index()
        {
            int loggeId = 1011;
            var allPosts = await _context.Posts
                .Where( n=> !n.IsPrivat || n.UserId == loggeId )
                .Include(n => n.User)
                .Include(n => n.Likes)
                .Include(n => n.Comments.Select(p => p.User))
                .Include(n => n.Favorites)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();

            return View(allPosts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(PostVM post)
        {
            //Get the logged in user
            int loggedInUser = 1011;

            //Create a new post
            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdate = DateTime.UtcNow,
                ImageUrl = "",
                NrOfReports = 0,
                UserId = loggedInUser
            };

            //Check and save the image
            if (post.Image != null && post.Image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (post.Image.ContentType.Contains("image"))
                {
                    string rootFolderPathImages = Path.Combine(rootFolderPath, "images/uploaded");
                    Directory.CreateDirectory(rootFolderPathImages);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.Image.FileName);
                    string filePath = Path.Combine(rootFolderPathImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await post.Image.CopyToAsync(stream);

                    //Set the URL to the newPost object
                    newPost.ImageUrl = "/images/uploaded/" + fileName;
                }
            }

            _context.Posts.Add(newPost);
            //Add the post to the database
            // await _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            //Redirect to the index page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> TogglePostLike(PostLikeVM postLikeVM)
        {
            int loggedInUser = 1011;

            //Check if the user has already liked the post
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(n => n.UserId == loggedInUser && n.PostId == postLikeVM.PostId);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
            } else
            {
                //Add the like to the database
                var newLike = new Like
                {
                    PostId = postLikeVM.PostId,
                    UserId = loggedInUser
                };
                _context.Likes.Add(newLike);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddPostComment(PostCommentVM postCommentVM)
        {
            int loggedInUserId = 1011;

            //Creat a post object
            var newComment = new Comment()
            {
                UserId = loggedInUserId,
                PostId = postCommentVM.PostId,
                Content = postCommentVM.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> RemovePostComment(RemoveCommentVM removeCommentVM)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(c => c.Id == removeCommentVM.CommentId);

            if (commentDb != null)
            {
                _context.Comments.Remove(commentDb);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> TogglePostFavorite(PostFavoriteVM postFavoriteVM)
        {
            int loggedInUserId = 1011;

            //check if user has already favorited the post
            var favorite = await _context.Favorites
                .Where(l => l.PostId == postFavoriteVM.PostId && l.UserId == loggedInUserId)
                .FirstOrDefaultAsync();

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newFavorite = new Favorite()
                {
                    DateCreated = DateTime.Now,
                    PostId = postFavoriteVM.PostId,
                    UserId = loggedInUserId
                };
                 _context.Favorites.Add(newFavorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> TogglePostVisibility(PostVisibilityVM postVisibilityVM)
        {
            int loggedInUserId = 1011;

            //get post by id and loggedin user id
            var post = await _context.Posts
                .FirstOrDefaultAsync(l => l.PostId == postVisibilityVM.PostId && l.UserId == loggedInUserId);

            if (post != null)
            {
                post.IsPrivat = !post.IsPrivat;
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}