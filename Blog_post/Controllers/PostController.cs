using Blog_post.Data;
using Blog_post.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blog_post.Enums;

namespace Blog_post.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Post> posts = _context.posts.Where(x => x.StatusId != StatusEnum.Draft);
            if(posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var post = _context.posts.Find(id);

            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
