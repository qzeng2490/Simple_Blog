using H622.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H622.DAL
{
    public interface IBlogRepository
    {
        IQueryable<Post> getPosts();
        Post getPosts(int id);

        IEnumerable<Tag> getTags();
    }
}
