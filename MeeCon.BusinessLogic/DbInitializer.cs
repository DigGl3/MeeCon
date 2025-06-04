using MeeCon.Domain.Model.Subscription;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MeeCon.BusinessLogic
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            try
            {
                // Force close all connections to the database
                context.Database.ExecuteSqlCommand(
                    @"ALTER DATABASE [MeeConAppDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                // Seed subscription plans
                var subscriptions = new List<Subscription>
                {
                    new Subscription
                    {
                        Name = "Ad-Free Plan",
                        Price = 2.00m,
                        PostLimit = 0, // No post limit
                        IsAdFree = true,
                        Description = "Enjoy an ad-free experience on our platform.",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Subscription
                    {
                        Name = "Unlimited Posts",
                        Price = 5.00m,
                        PostLimit = -1, // Unlimited posts
                        IsAdFree = false,
                        Description = "Post as much as you want with no limits.",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Subscription
                    {
                        Name = "Premium Plan",
                        Price = 5.00m,
                        PostLimit = 25,
                        IsAdFree = true,
                        Description = "Get 25 posts per month and an ad-free experience.",
                        CreatedAt = DateTime.UtcNow
                    }
                };

                context.Subscriptions.AddRange(subscriptions);
                context.SaveChanges();

                // Reset database to multi-user mode
                context.Database.ExecuteSqlCommand(
                    @"ALTER DATABASE [MeeConAppDB] SET MULTI_USER");
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                System.Diagnostics.Debug.WriteLine($"Error during database initialization: {ex.Message}");
                
                // Make sure to reset to multi-user mode even if there's an error
                try
                {
                    context.Database.ExecuteSqlCommand(
                        @"ALTER DATABASE [MeeConAppDB] SET MULTI_USER");
                }
                catch { }
                
                throw; // Re-throw the original exception
            }

            // Seed test user if none exists
            if (!context.Users.Any())
            {
                var testUser = new UDbModel
                {
                    Username = "testuser",
                    Password = "hashedpassword", // In production, use proper password hashing
                    ProfileImage = "/images/avatar/default.png",
                    IsDeleted = false
                };

                context.Users.Add(testUser);
                context.SaveChanges();
            }

            base.Seed(context);
        }
    }
}
