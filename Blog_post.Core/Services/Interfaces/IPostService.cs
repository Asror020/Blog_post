using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IPostService : IBasePostService
    {
        List<Post> GetLastEight();
    }
}
