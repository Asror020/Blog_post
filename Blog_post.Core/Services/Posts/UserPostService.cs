using Blog_post.Data;
using Blog_post.Enums;
using Blog_post.Models;
using Blog_post.Services.Interfaces;
using Blog_post.Services.Posts;
using Microsoft.EntityFrameworkCore;

namespace Blog_post.Services.User
{
    public class UserPostService :BasePostService, IUserPostService
    {
        public UserPostService(ApplicationDbContext context) : base(context)
        {
        }
        public void EditPost(Post post)
        {
            _context.posts.Update(post);
            _context.SaveChanges();
        }
        public void DeletePost(Post post)
        {
            _context.posts.Remove(post);
            _context.SaveChanges();
        }
        public bool PostExists(int id)
        {
            return _context.posts.Any(x => x.Id == id);
        }
        public List<Post> GetByAuothorId(string authorId)
        {
            var posts = _context.posts.Include(x => x.Status).Where(p => p.AuthorId == authorId).OrderByDescending(x => x.CreatedDate).ToList();
            return posts;
        }

        public Post Details(int id)
        {
            var post = _context.posts.Include(x => x.Status).Where(x => x.Id == id).FirstOrDefault();
            return post;
        }

        public void CreatePost(Post post)
        {
            _context.posts.Add(post);
            _context.SaveChanges();
        }
    }
}
