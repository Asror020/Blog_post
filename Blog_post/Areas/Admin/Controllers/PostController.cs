using Blog_post.Data;
using Blog_post.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_post.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.posts.Where(x => x.StatusId != StatusEnum.Draft).ToList();
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        public IActionResult Approve(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.posts.Find(id);
            if(post == null)
            {
                return NotFound();
            }
            post.StatusId = StatusEnum.Approve;
            _context.posts.Update(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Reject(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.posts.FirstOrDefault(i => i.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            post.StatusId = StatusEnum.Reject;
            _context.posts.Update(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
