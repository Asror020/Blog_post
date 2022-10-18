using Microsoft.AspNetCore.Mvc;
using Blog_post.Services.Interfaces;

namespace Blog_post.Controllers
{
    public class PostController : Controller
    {
        private IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            var posts = _postService.GetLastEight();
            if(posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }
        public IActionResult Details(int id)
        {
            var post = _postService.GetById(id);

            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
