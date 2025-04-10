using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeeCon.Domain.Model.User;

namespace MeeCon.BusinessLogic
{
    public class DataContext : DbContext
    {
        public DbSet<UDbModel> Users { get; set; }

        public DataContext() : base("name=MeeConAppDB")
        {
            Database.SetInitializer<DataContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UDbModel>()
                .HasKey(u => u.Id)
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<UDbModel>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(32);

        }
    }
}
