using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IAdminPostService : IBasePostService
    {
        List<Post> GetAll();
        void Approve(Post post);
        void Reject(Post post);
    }
}
