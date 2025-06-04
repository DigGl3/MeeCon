using MeeCon.Domain.Model.Home;
using MeeCon.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllVisiblePostsAsync(int userId);
        Task CreatePostAsync(PostVM post, int userId);
        Task ToggleLikeAsync(int postId, int userId);
        Task AddCommentAsync(PostCommentVM comment, int userId);
        Task RemoveCommentAsync(int commentId);
        Task ToggleFavoriteAsync(int postId, int userId);
        Task ToggleVisibilityAsync(int postId, int userId);
        Task AddReportAsync(int postId, int userId);
    }

}
