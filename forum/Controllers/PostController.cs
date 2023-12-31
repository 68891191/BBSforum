using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.Data;
using Forum.Models;

namespace forum.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumDbContext _context;

        public PostController(ForumDbContext context)
        {
            _context = context;
        }

        private async Task<User?> GetUserAsync()
        {
            // Retrieve the user based on their session token
            string? token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return null;
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.token == token);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            // Get the user from the session
            var userSession = await GetUserAsync();
            if (userSession == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Retrieve and display all posts
            var posts = await _context.Post.ToListAsync();
            var idToName = new Dictionary<int, string>();
            foreach (var post in posts)
            {
                var user = await _context.User.FindAsync(post.userId);
                if (user != null)
                    idToName[post.userId] = user.name + " " + user.lastName;
            }

            ViewData["idToName"] = idToName;

            // Return the view if the Post entity set is not null
            return _context.Post != null ?
                        View(await _context.Post.ToListAsync()) :
                        Problem("Entity set 'ForumDbContext.Post' is null.");
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Get the user from the session
            var userSession = await GetUserAsync();
            if (userSession == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            // Retrieve and display post details
            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            // Get the user from the session
            var userSession = await GetUserAsync();
            if (userSession == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection postForm)
        {
            // Get the user from the session
            var userSession = await GetUserAsync();
            if (userSession == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Check and create a new tag if it doesn't exist
            var tag = _context.Tag.FirstOrDefault(t => t.name == postForm["tag"].ToString());
            if (tag == null)
            {
                tag = new Tag
                {
                    name = postForm["tag"].ToString(),
                };
                _context.Add(tag);
                await _context.SaveChangesAsync();
            }
            var newTag = _context.Tag.FirstOrDefault(t => t.name == postForm["tag"].ToString());
            if (newTag == null)
            {
                return Problem("Tag not found.");
            }

            // Create a new post and add it to the database
            var post = new Post
            {
                userId = userSession.id,
                title = postForm["title"].ToString(),
                content = postForm["content"].ToString(),
                tagId = newTag.id,
            };

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Get the user from the session
            var userSession = await GetUserAsync();

            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Check if the user has permission to edit
            if (userSession == null || userSession.role != "admin" && userSession.id != post.userId)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,userId,title,content,createdAt")] Post post)
        {
            if (id != post.id)
            {
                return NotFound();
            }

            // Get the user from the session
            var userSession = await GetUserAsync();
            if (userSession == null || userSession.role != "admin" && userSession.id != post.userId)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            // Get the user from the session
            var userSession = await GetUserAsync();

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            // Check if the user has permission to delete
            if (userSession == null || userSession.role != "admin" && userSession.id != post.userId)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'ForumDbContext.Post' is null.");
            }

            // Get the user from the session
            var userSession = await GetUserAsync();
            var post = await _context.Post.FindAsync(id);

            if (post != null)
            {
                // Check if the user has permission to delete
                if (userSession == null || userSession.role != "admin" && userSession.id != post.userId)
                {
                    return RedirectToAction("SignIn", "Auth");
                }

                _context.Post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return (_context.Post?.Any(e => e.id == id)).GetValueOrDefault();
        }
        // Action to give a like to a post
        public async Task<IActionResult> GiveLike(int id)
        {
            var user = await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Increase the like count for the post
            var post = _context.Post.Where(p => p.id == id).ToList()[0];
            post.likes += 1;
            _context.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // Action to give a dislike to a post
        public async Task<IActionResult> GiveDislike(int id)
        {
            var user = await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Increase the dislike count for the post
            var post = _context.Post.Where(p => p.id == id).ToList()[0];
            post.dislikes += 1;
            _context.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
