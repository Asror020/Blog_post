using Blog_post.Data;
using Blog_post.Models;
using Blog_post.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog_post.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = _context.posts.Where(x => x.AuthorId == user);
            return View(posts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMV posts)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                Post newpost = new()
                {
                    Title = posts.Title,
                    Text = posts.Text,
                    CreatedDate = DateTime.Now,
                    AuthorId = user
                };
                _context.posts.Add(newpost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posts);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _context.posts.Find(id);
            return View(post);
        }
        public IActionResult Edit(int id, [Bind("Id,Title,Text,CreateDate,AuthorId")] EditMV postVM)
        {
            var post = _context.posts.Find(id);
            if (ModelState.IsValid)
            {
                try
                {
                    post.Title = postVM.Title;
                    post.Text = postVM.Text;
                    _context.posts.Update(post);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(postVM.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.posts.Find(id);
            return View(post);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var post = _context.posts.Find(id);
            if(post == null) { return NotFound(); }
            _context.posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool PostExists(int id)
        {
            return _context.posts.Any(e => e.Id == id);
        }
    }
}
