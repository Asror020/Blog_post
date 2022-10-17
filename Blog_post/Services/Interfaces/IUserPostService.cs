using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IUserPostService
    {
        List<Post> GetByAuothorId(string authorId);
        Post Details(int id);
        void CreatePost(Post post);
        Post GetById(int id);
        void EditPost(Post post);
        void DeletePost(Post post);
        bool PostExists (int id);
    }
}
