using MeeCon.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeeCon.Web.Controllers.Data
{
	public class AppDBContext: DbContext
	{
		public AppDBContext() : base("MeeConDB")
        {
        }
        public DbSet<Post> Posts { get; set; }
    }
}