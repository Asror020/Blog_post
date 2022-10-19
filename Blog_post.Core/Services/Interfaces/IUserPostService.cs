using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IUserPostService : IBasePostService
    {
        List<Post> GetByAuothorId(string authorId);
        Post Details(int id);
        void CreatePost(Post post);
        void EditPost(Post post);
        void DeletePost(Post post);
        bool PostExists (int id);
    }
}
