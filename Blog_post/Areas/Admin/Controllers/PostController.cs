using Blog_post.Data;
using Blog_post.Enums;
using Blog_post.Services.Admin;
using Blog_post.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_post.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private IAdminPostService _adminPostService;
        public PostController(IAdminPostService adminService)
        {
            _adminPostService = adminService;
        }

        public IActionResult Index()
        {
            var posts = _adminPostService.GetAll();
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }
        public IActionResult Details(int id)
        {
            var post = _adminPostService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var post = _adminPostService.GetById(id);
            if(post == null)
            {
                return NotFound();
            }
            _adminPostService.Approve(post);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var post = _adminPostService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            _adminPostService.Reject(post);
            return RedirectToAction(nameof(Index));
        }
    }
}
