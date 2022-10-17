using Blog_post.Data;
using Blog_post.Services.Interfaces;
using Blog_post.Models;
using Blog_post.Enums;

namespace Blog_post.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Post GetById(int id)
        {
            var post = _context.posts.FirstOrDefault(x => x.Id == id);
            return post;
        }

        public List<Post> GetLastEight()
        {
            var posts = _context.posts.Where(x => x.StatusId == StatusEnum.Approve).OrderByDescending(x => x.CreatedDate).Take(8);
            return posts.ToList();
        }
    }
}
