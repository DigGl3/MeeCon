using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeeCon.Domain.Model.User;
using MeeCon.Web.Models;

namespace MeeCon.BusinessLogic
{
    public class DataContext : DbContext
    {
        public DbSet<UDbModel> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

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
        }
    }
}
