using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeeCon.Domain.Model;
using MeeCon.Domain.Model.User;
using MeeCon.Domain.Model.Post;
using MeeCon.Domain.Model.Subscription;
using MeeCon.Domain.Model.Cart;

namespace MeeCon.BusinessLogic
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=MeeConAppDB")
        {
        }

        public DbSet<UDbModel> Users { get; set; }
        public DbSet<MeeCon.Domain.Model.Post.Post> Posts { get; set; }
        public DbSet<MeeCon.Domain.Model.Post.Like> Likes { get; set; }
        public DbSet<MeeCon.Domain.Model.Post.Comment> Comments { get; set; }
        public DbSet<MeeCon.Domain.Model.Post.Favorite> Favorites { get; set; }
        public DbSet<MeeCon.Domain.Model.Post.Report> Reports { get; set; }
        public DbSet<MeeCon.Domain.Model.Subscription.Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UDbModel>()
                .HasKey(u => u.UserId)
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<UDbModel>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(32);

            modelBuilder.Entity<UDbModel>()
                .Property(u => u.IsDeleted)
                .IsRequired();

            modelBuilder.Entity<UDbModel>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.User)  
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Like>()
                .HasKey(l => new { l.PostId, l.UserId});
            modelBuilder.Entity<MeeCon.Domain.Model.Post.Like>()
                .HasRequired(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Like>()
                .HasRequired(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(true);

            //Comment 
            modelBuilder.Entity<MeeCon.Domain.Model.Post.Comment>()
                .HasRequired(l => l.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(l => l.PostId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Comment>()
                .HasRequired(l => l.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(true);

            //Favorite 
            modelBuilder.Entity<MeeCon.Domain.Model.Post.Favorite>()
                .HasKey(f => new { f.PostId, f.UserId });

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Favorite>()
                .HasRequired(f => f.Post)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PostId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Favorite>()
                .HasRequired(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(true);

            //Reports
            modelBuilder.Entity<MeeCon.Domain.Model.Post.Report>()
                .HasKey(f => new { f.PostId, f.UserId });

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Report>()
                .HasRequired(f => f.Post)
                .WithMany(p => p.Reports)
                .HasForeignKey(f => f.PostId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MeeCon.Domain.Model.Post.Report>()
                .HasRequired(f => f.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(true);

            // Subscription relationships
            modelBuilder.Entity<UserSubscription>()
                .HasRequired(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSubscription>()
                .HasRequired(us => us.Subscription)
                .WithMany()
                .HasForeignKey(us => us.SubscriptionId);

            // Cart relationships
            modelBuilder.Entity<CartItem>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasRequired(c => c.Subscription)
                .WithMany()
                .HasForeignKey(c => c.SubscriptionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
