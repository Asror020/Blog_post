using Blog_post.Data;
using Blog_post.Models;
using Blog_post.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog_post.Services.Posts
{
    public class BasePostService : IBasePostService
    {
        protected readonly ApplicationDbContext _context;
        public BasePostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Post GetById(int id)
        {
            var post = _context.posts.Include(x => x.Status).FirstOrDefault(x => x.Id == id);
            return post;
        }
    }
}
