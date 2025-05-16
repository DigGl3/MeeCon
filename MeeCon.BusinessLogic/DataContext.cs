using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeeCon.Domain.Model;
using MeeCon.Domain.Model.User;
using MeeCon.Web.Models;

namespace MeeCon.BusinessLogic
{
    public class DataContext : DbContext
    {
        public DbSet<UDbModel> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DataContext() : base("name=MeeConAppDB")
        {
            Database.SetInitializer<DataContext>(null);
        }

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
                .HasMany(u => u.Posts)
                .WithRequired(p => p.User)  
                .HasForeignKey(p => p.UserId);


            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.PostId, l.UserId});
            modelBuilder.Entity<Like>()
                .HasRequired(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Like>()
                .HasRequired(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete();

            //Comment 
            modelBuilder.Entity<Comment>()
                .HasRequired(l => l.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(l => l.PostId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Comment>()
                .HasRequired(l => l.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);

        }
    }
}
