using System.Collections.Generic;
using System.Linq;

namespace Funemag.Models.Repository
{
    public class PostRepository : Repository<Post>
    {
        public List<Post> GetByTitle(string title)
        {
            return DbSet.Where(p => p.Title.Contains(title)).ToList();
        }

        public IQueryable<Post> GetBySearch(string searchString)
        {
            return DbSet.Where(post => post.Title.Contains(searchString)).OrderByDescending(post => post.Date);
        }

        public IQueryable<Post> GetByPlatform(string platform)
        {
            return DbSet.Where(post => post.Platforms.Any(p => p.Name.Equals(platform)));
        }

        public IQueryable<Post> Add(string searchString)
        {
            return DbSet.Where(post => post.Title.Contains(searchString)).OrderByDescending(post => post.Date);
        }
    }
}