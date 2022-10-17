using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IPostService
    {
        List<Post> GetLastEight();
        Post GetById(int id);
    }
}
