using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H622.Models;

namespace H622.DAL
{
    public class BlogRepository : IBlogRepository
    {
        BlogContext _ctx;
        public BlogRepository(BlogContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Post> getPosts()
        {
            return _ctx.Posts
                       .OrderByDescending(p => p.CreateTime);
                        
        }

        public Post getPosts(int postid)
        {
            return _ctx.Posts.Where(x => x.ID == postid).Single();
        }

        public IEnumerable<Tag> getTags()
        {
            return _ctx.Tags.ToList();
        }
    }
}