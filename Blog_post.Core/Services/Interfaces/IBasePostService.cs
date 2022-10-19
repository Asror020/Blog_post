using Blog_post.Models;

namespace Blog_post.Services.Interfaces
{
    public interface IBasePostService
    {
        Post GetById(int id);
    }
}
