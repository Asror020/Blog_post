using Blog_post.Data;
using Blog_post.Services.Interfaces;
using Blog_post.Models;
using Blog_post.Enums;
using Blog_post.Services.Posts;

namespace Blog_post.Services
{
    public class PostService :BasePostService, IPostService
    {
        public PostService(ApplicationDbContext context): base(context)
        {
        }
        public List<Post> GetLastEight()
        {
            var posts = _context.posts.Where(x => x.StatusId == StatusEnum.Approve).OrderByDescending(x => x.CreatedDate).Take(8);
            return posts.ToList();
        }
    }
}
