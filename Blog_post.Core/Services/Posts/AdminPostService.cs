using Blog_post.Data;
using Blog_post.Enums;
using Blog_post.Models;
using Blog_post.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog_post.Services.Posts
{
    public class AdminPostService : BasePostService,IAdminPostService
    {
        public AdminPostService(ApplicationDbContext context): base(context)
        {
        }
        public List<Post> GetAll()
        {
            var posts = _context.posts.Include(x => x.Status).Where(x => x.StatusId != StatusEnum.Draft).OrderByDescending(x => x.CreatedDate).ToList();
            return posts;
        }
        public void Approve(Post post)
        {
            post.StatusId = StatusEnum.Approve;
            _context.SaveChanges();
        }
        public void Reject(Post post)
        {
            post.StatusId = StatusEnum.Reject;
            _context.SaveChanges();
        }
    }
}
