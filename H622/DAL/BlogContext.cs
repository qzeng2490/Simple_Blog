using H622.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace H622.DAL
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("name=BlogContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}