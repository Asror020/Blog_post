using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IAdminPostService
    {
        List<Post> GetAll();
        Post GetById(int id);
        void Approve(Post post);
        void Reject(Post post);
    }
}
