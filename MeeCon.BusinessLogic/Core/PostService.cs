using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model;
using MeeCon.Domain.Model.Home;
using MeeCon.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService()
        {
            _context = new DataContext();
        }

        public async Task<List<Post>> GetAllVisiblePostsAsync(int userId)
        {
            return await _context.Posts
                .Where(n => (!n.IsPrivate || n.UserId == userId) && n.Reports.Count < 5)
                .Include(n => n.User)
                .Include(n => n.Likes)
                .Include(n => n.Comments.Select(p => p.User))
                .Include(n => n.Favorites)
                .Include(n => n.Reports)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
        }

        public async Task CreatePostAsync(PostVM post, int userId)
        {
            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ImageUrl = "",
                UserId = userId,
                NrOfReports = 0
            };

            if (post.Image != null && post.Image.Length > 0 && post.Image.ContentType.Contains("image"))
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploaded");
                Directory.CreateDirectory(rootFolderPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.Image.FileName);
                string filePath = Path.Combine(rootFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await post.Image.CopyToAsync(stream);

                newPost.ImageUrl = "/images/uploaded/" + fileName;
            }

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleLikeAsync(int postId, int userId)
        {
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(n => n.UserId == userId && n.PostId == postId);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
            }
            else
            {
                var newLike = new Like
                {
                    PostId = postId,
                    UserId = userId
                };
                _context.Likes.Add(newLike);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(PostCommentVM comment, int userId)
        {
            var newComment = new Comment
            {
                PostId = comment.PostId,
                UserId = userId,
                Content = comment.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleFavoriteAsync(int postId, int userId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.PostId == postId && f.UserId == userId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
            }
            else
            {
                var newFavorite = new Favorite
                {
                    PostId = postId,
                    UserId = userId,
                    DateCreated = DateTime.UtcNow
                };
                _context.Favorites.Add(newFavorite);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ToggleVisibilityAsync(int postId, int userId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId);

            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddReportAsync(int postId, int userId)
        {
            var newReport = new Report
            {
                PostId = postId,
                UserId = userId,
                DateCreated = DateTime.UtcNow
            };

            _context.Reports.Add(newReport);
            await _context.SaveChangesAsync();
        }
    }
}
