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
        private IUserPostService _userPostService;
        public PostController(IUserPostService context)
        {
            _userPostService = context;
        }
        public IActionResult Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = _userPostService.GetByAuothorId(user);
            return View(posts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateMV posts, string submitbtn)
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
                _userPostService.CreatePost(newpost);
                return RedirectToAction("Index");
            }
            return View(posts);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var post = _userPostService.Details(id.Value);
            if(post == null || post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            return View(post);
        }
        public IActionResult Edit(int id)
        {
            var post = _userPostService.Details(id);
            if (post == null || post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            if (post.StatusId == Enums.StatusEnum.WaitingForApproval)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PostEditMV postVM, string submitbtn)
        {
            var post = _userPostService.Details(id);
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
                    _userPostService.EditPost(post);
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
            var post = _userPostService.Details(id);
            if (post == null || post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            if(post.StatusId == Enums.StatusEnum.WaitingForApproval)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var post = _userPostService.Details(id);
            if(post == null) { return NotFound(); }
            _userPostService.DeletePost(post);
            return RedirectToAction("Index");
        }
        private bool PostExists(int id)
        {
            return _userPostService.PostExists(id);
        }
    }
}
