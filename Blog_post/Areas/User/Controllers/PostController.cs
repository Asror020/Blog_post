using Blog_post.Data;
using Blog_post.Models;
using Blog_post.Services.Interfaces;
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
        private IUserPostService _userService;
        public PostController(IUserPostService context)
        {
            _userService = context;
        }
        public IActionResult Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = _userService.GetByAuothorId(user);
            return View(posts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostCreateMV posts, string submitbtn)
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
                if(submitbtn.Equals("Save as Draft"))
                {
                    newpost.StatusId = Enums.StatusEnum.Draft;
                }
                else if (submitbtn.Equals("Submit to check"))
                {
                    newpost.StatusId = Enums.StatusEnum.WaitingForApproval;
                }
                _userService.CreatePost(newpost);
                return RedirectToAction("Index");
            }
            return View(posts);
        }
        public IActionResult Details(int id)
        {
            var post = _userService.GetById(id);
            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        public IActionResult Edit(int id)
        {
            var post = _userService.GetById(id);
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PostEditMV postVM, string submitbtn)
        {
            var post = _userService.GetById(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (post == null)
                    {
                        return NotFound();
                    }
                    post.Title = postVM.Title;
                    post.Text = postVM.Text;

                    if (submitbtn.Equals("Save as Draft"))
                    {
                        post.StatusId = Enums.StatusEnum.Draft;
                    }
                    else if (submitbtn.Equals("Submit to check"))
                    {
                        post.StatusId = Enums.StatusEnum.WaitingForApproval;
                    }
                    _userService.EditPost(post);
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
        public IActionResult Delete(int id)
        {
            var post = _userService.GetById(id);
            return View(post);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var post = _userService.GetById(id);
            if(post == null) { return NotFound(); }
            _userService.DeletePost(post);
            return RedirectToAction("Index");
        }
        private bool PostExists(int id)
        {
            return _userService.PostExists(id);
        }
    }
}
