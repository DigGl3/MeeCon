using MeeCon.Domain.Model.User;
using MeeCon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic
{
    public class DbInitializer
    {
        public static void Seed(DataContext appDbContext)
        {
            if (!appDbContext.Users.Any() && !appDbContext.Posts.Any()) {
                var newUser = new UDbModel()
                {
                    Username = "admin",
                    ProfileImage = "-wwwroot/images/avatar/user.png"

                };
                appDbContext.Users.Add(newUser);
                appDbContext.SaveChanges();

                var newPost = new Post()
                {
                    Content = "This is a sample post.",
                    ImageUrl = "",
                    NrOfReports = 0,
                    DateCreated = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow,
                    UserId = newUser.UserId
                };

                var newPostWithImage = new Post()
                {
                    Content = "This is a sample post with an image.",
                    ImageUrl = "-wwwroot/images/placeholders/post-placeholder.jpg",
                    NrOfReports = 0,
                    DateCreated = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow,
                    UserId = newUser.UserId
                };
                appDbContext.Posts.Add(newPost);
                appDbContext.Posts.Add(newPostWithImage);
                appDbContext.SaveChanges();
            }
        }
    }
}
