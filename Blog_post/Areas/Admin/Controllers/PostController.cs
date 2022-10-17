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
        private IAdminPostService _adminService;
        public PostController(IAdminPostService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            var posts = _adminService.GetAll();
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }
        public IActionResult Details(int id)
        {
            var post = _adminService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var post = _adminService.GetById(id);
            if(post == null)
            {
                return NotFound();
            }
            _adminService.Approve(post);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var post = _adminService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            _adminService.Reject(post);
            return RedirectToAction(nameof(Index));
        }
    }
}
